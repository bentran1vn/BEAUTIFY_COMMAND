using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Feedbacks;
public class CreateFeedbackCommandHandler : ICommandHandler<CONTRACT.Services.Feedbacks.Commands.CreateFeedbackCommand>
{
    private readonly IRepositoryBase<CustomerSchedule, Guid> _customerScheduleRepository;
    private readonly IMediaService _mediaService;
    private readonly IRepositoryBase<OrderFeedback, Guid> _orderFeedbackRepository;
    private readonly IRepositoryBase<Order, Guid> _orderRepository;
    private readonly IRepositoryBase<Service, Guid> _serviceRepository;
    private readonly IRepositoryBase<Feedback, Guid> _scheduleFeedbackRepository;
    private readonly IRepositoryBase<Staff, Guid> _staffRepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxRepository;

    public CreateFeedbackCommandHandler(IRepositoryBase<Feedback, Guid> scheduleFeedbackRepository,
        IRepositoryBase<OrderFeedback, Guid> orderFeedbackRepository, IRepositoryBase<Order, Guid> orderRepository,
        IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepository,
        IRepositoryBase<Staff, Guid> staffRepository, IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository,
        IMediaService mediaService, IRepositoryBase<Service, Guid> serviceRepository)
    {
        _scheduleFeedbackRepository = scheduleFeedbackRepository;
        _orderFeedbackRepository = orderFeedbackRepository;
        _orderRepository = orderRepository;
        _customerScheduleRepository = customerScheduleRepository;
        _staffRepository = staffRepository;
        _triggerOutboxRepository = triggerOutboxRepository;
        _mediaService = mediaService;
        _serviceRepository = serviceRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Feedbacks.Commands.CreateFeedbackCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository
            .FindAll(x => x.Id.Equals(request.OrderId) && !x.IsDeleted)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null) return Result.Failure(new Error("404", "Order not found"));

        if (order.Status != Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("404", "Order not found"));

        var customerSchedule = await _customerScheduleRepository
            .FindAll(x => x.OrderId.Equals(request.OrderId))
            .Include(x => x.Doctor)
            .ThenInclude(y => y.User)
            .ToListAsync(cancellationToken);

        if (customerSchedule == null || !customerSchedule.Any())
            return Result.Failure(new Error("404", "Customer schedule not found"));

        // if(customerSchedule.Any(x => x.Status != Constant.OrderStatus.ORDER_COMPLETED))
        // {
        //     return Result.Failure(new Error("400", "Customer schedule not completed"));
        // }

        if (customerSchedule.Count != request.ScheduleFeedbacks.Count)
            return Result.Failure(new Error("500", "Missing schedule feedback"));

        var scheList = request.ScheduleFeedbacks.Select(x => x.CustomerScheduleId);

        var feedbackCheck = await _scheduleFeedbackRepository
            .FindAll(x => scheList.Contains(x.CustomerScheduleId))
            .ToListAsync(cancellationToken);

        var feedbackOrder = await _orderFeedbackRepository.FindAll(x => x.OrderId.Equals(request.OrderId))
            .FirstOrDefaultAsync(cancellationToken);

        if (feedbackCheck.Count == request.ScheduleFeedbacks.Count || feedbackOrder != null)
            return Result.Failure(new Error("500", "Already Feedback"));

        Dictionary<Guid, (int Sum, int Count)> doctorRatings = new();

        var feedbacks = request.ScheduleFeedbacks.Select(x =>
        {
            var schedule = customerSchedule.FirstOrDefault(y => y.Id.Equals(x.CustomerScheduleId));

            if (schedule != null)
            {
                if (doctorRatings.TryGetValue(schedule.Doctor!.User.Id, out var current))
                    doctorRatings[schedule.Doctor!.User.Id] = (current.Sum + x.Rating, current.Count + 1);
                else
                    doctorRatings[schedule.Doctor!.User.Id] = (x.Rating, 1);
            }

            var feedback = new Feedback
            {
                Id = Guid.NewGuid(),
                Content = x.Content,
                Rating = x.Rating,
                CustomerScheduleId = x.CustomerScheduleId
            };
            
            schedule.FeedbackId = feedback.Id;
            
            return feedback;
        }).ToList();

        _scheduleFeedbackRepository.AddRange(feedbacks);

        var normalizedRatings = doctorRatings.ToDictionary(
            pair => pair.Key,
            pair => Math.Clamp((int)Math.Round((double)pair.Value.Sum / pair.Value.Count), 1, 5)
        );

        var staff = await _staffRepository.FindAll(
            x => !x.IsDeleted && x.Role.Name == Constant.Role.DOCTOR 
                && normalizedRatings.Keys.Contains(x.Id)
        ).AsNoTracking().ToListAsync(cancellationToken);
        
        var staffFeedback = await _customerScheduleRepository.FindAll(
            x => !x.IsDeleted
                 && normalizedRatings.Keys.Contains(x.DoctorId)
                && x.FeedbackId != null
        ).AsNoTracking().ToListAsync(cancellationToken);

        if (staff == null || !staff.Any()) throw new Exception("Staff not found");

        List<TriggerOutbox.DoctorFeedback> doctorFeedbacks = new();
        
        staff = staff.Select(x =>
        {
            if (normalizedRatings.TryGetValue(x.Id, out var rating))
            {
                var previousRatingCount = staffFeedback.Count(y => y.DoctorId == x.Id);
                x.Rating = (x.Rating * previousRatingCount + rating) / (previousRatingCount + 1);
                doctorFeedbacks.Add(new TriggerOutbox.DoctorFeedback
                {
                    FeedbackId = x.Id,
                    NewRating = x.Rating, // the newest rating, after update
                    DoctorId = x.Id,
                    Content = ""
                });
            }
            return x;
        }).ToList();

        _staffRepository.UpdateRange(staff);
        _customerScheduleRepository.UpdateRange(customerSchedule);

        var servicesCoverImageTasks = request.Images.Select(_mediaService.UploadImageAsync);

        var coverImageUrls = await Task.WhenAll(servicesCoverImageTasks);

        var orderFeedback = new OrderFeedback
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            Rating = request.Rating,
            OrderId = request.OrderId
        };

        _orderFeedbackRepository.Add(orderFeedback);
        
        order.OrderFeedbackId = orderFeedback.Id;
        
        var previousOrderRatingCount = await _orderRepository.FindAll(
            x => !x.IsDeleted
                 && x.ServiceId == order.ServiceId
                 && x.OrderFeedbackId != null
        ).AsNoTracking().CountAsync(cancellationToken);
        
        _orderRepository.Update(order);
        
        var currentService = await _serviceRepository.FindAll(
            x => !x.IsDeleted && x.Id == order.ServiceId
        ).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        if (currentService == null)
        {
            return Result.Failure(new Error("500", "Service not found"));
        }
        
        var newServiceRating = 0.0;
        if (previousOrderRatingCount > 0) {
            newServiceRating = (currentService.Rating * previousOrderRatingCount + request.Rating) / (previousOrderRatingCount + 1);
        } else {
            newServiceRating = request.Rating;
        }
        
        currentService.Rating = newServiceRating;
        
        _serviceRepository.Update(currentService);

        var trigger = TriggerOutbox.CreateFeedbackEvent(
            orderFeedback.Id,
            (Guid)order.ServiceId!,
            coverImageUrls,
            orderFeedback.Content,
            orderFeedback.Rating,
            order.Customer!,
            orderFeedback.CreatedOnUtc,
            newServiceRating,
            doctorFeedbacks
        );

        _triggerOutboxRepository.Add(trigger);

        return Result.Success("Feedback successfully !");
    }
}