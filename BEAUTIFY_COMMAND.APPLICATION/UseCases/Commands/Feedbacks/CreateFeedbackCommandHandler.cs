namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Feedbacks;

public class CreateFeedbackCommandHandler : ICommandHandler<CONTRACT.Services.Feedbacks.Commands.CreateFeedbackCommand>
{
    private readonly IRepositoryBase<Feedback, Guid> _scheduleFeedbackRepository;
    private readonly IRepositoryBase<OrderFeedback, Guid> _orderFeedbackRepository;
    private readonly IRepositoryBase<Order, Guid> _orderRepository;
    private readonly IRepositoryBase<CustomerSchedule, Guid> _customerScheduleRepository;
    private readonly IRepositoryBase<Staff, Guid> _staffRepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxRepository;
    private readonly IMediaService _mediaService;

    public CreateFeedbackCommandHandler(IRepositoryBase<Feedback, Guid> scheduleFeedbackRepository, IRepositoryBase<OrderFeedback, Guid> orderFeedbackRepository, IRepositoryBase<Order, Guid> orderRepository, IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepository, IRepositoryBase<Staff, Guid> staffRepository, IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository, IMediaService mediaService)
    {
        _scheduleFeedbackRepository = scheduleFeedbackRepository;
        _orderFeedbackRepository = orderFeedbackRepository;
        _orderRepository = orderRepository;
        _customerScheduleRepository = customerScheduleRepository;
        _staffRepository = staffRepository;
        _triggerOutboxRepository = triggerOutboxRepository;
        _mediaService = mediaService;
    }

    public async Task<Result> Handle(CONTRACT.Services.Feedbacks.Commands.CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository
            .FindAll(x => x.Id.Equals(request.OrderId) && !x.IsDeleted)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return Result.Failure(new Error("404", "Order not found"));
        }

        if (order.Status != Constant.OrderStatus.ORDER_COMPLETED)
        {
            return Result.Failure(new Error("404", "Order not found"));
        }
        
        var customerSchedule = await _customerScheduleRepository
                .FindAll(x => x.OrderId.Equals(request.OrderId))
                .Include(x => x.Doctor)
                    .ThenInclude(y => y.User)
                .ToListAsync(cancellationToken);
        
        if (customerSchedule == null || !customerSchedule.Any())
        {
            return Result.Failure(new Error("404", "Customer schedule not found"));
        }
        
        // if(customerSchedule.Any(x => x.Status != Constant.OrderStatus.ORDER_COMPLETED))
        // {
        //     return Result.Failure(new Error("400", "Customer schedule not completed"));
        // }

        if (customerSchedule.Count != request.ScheduleFeedbacks.Count)
        {
            return Result.Failure(new Error("500", "Missing schedule feedback"));
        }
        
        // Track both ratings and counts in a single dictionary
        Dictionary<Guid, (int Sum, int Count)> doctorRatings = new();

        var feedbacks = request.ScheduleFeedbacks.Select(x =>
        {
            var schedule = customerSchedule.FirstOrDefault(y => y.Id.Equals(x.CustomerScheduleId));
            
            if (schedule != null)
            {
                if (doctorRatings.TryGetValue(schedule.DoctorId, out var current))
                {
                    doctorRatings[schedule.Doctor!.User.Id] = (current.Sum + x.Rating, current.Count + 1);
                }
                else
                {
                    doctorRatings[schedule.Doctor!.User.Id] = (x.Rating, 1);
                }
            }
    
            return new Feedback
            {
                Id = Guid.NewGuid(),
                Content = x.Content,
                Rating = x.Rating,
                CustomerScheduleId = x.CustomerScheduleId,
            };
        }).ToList();
        
        _scheduleFeedbackRepository.AddRange(feedbacks);
        
        Dictionary<Guid, int> normalizedRatings = doctorRatings.ToDictionary(
            pair => pair.Key,
            pair => Math.Clamp((int)Math.Round((double)pair.Value.Sum / pair.Value.Count), 1, 5)
        );

        var staff = await _staffRepository.FindAll(
            x => !x.IsDeleted && 
                 x.Role.Name == Constant.Role.DOCTOR && 
                 normalizedRatings.Keys.Contains(x.Id)
        ).ToListAsync(cancellationToken);
        
        if (staff == null || !staff.Any())
        {
            throw new Exception("Staff not found");
        }
        
        // Update staff ratings
        foreach (var staffMember in staff)
        {
            if (normalizedRatings.TryGetValue(staffMember.Id, out var rating))
            {
                staffMember.Rating = (staffMember.Rating + rating) / 2;
            }
        }
        
        _staffRepository.UpdateRange(staff);

        var servicesCoverImageTasks = request.Images.Select(_mediaService.UploadImageAsync);

        var coverImageUrls = await Task.WhenAll(servicesCoverImageTasks);

        var orderFeedback = new OrderFeedback()
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            Rating = request.Rating,
            OrderId = request.OrderId
        };
        
        _orderFeedbackRepository.Add(orderFeedback);
        
        var trigger = TriggerOutbox.CreateFeedbackEvent(
            orderFeedback.Id,
            order.Id,
            coverImageUrls,
            orderFeedback.Content,
            orderFeedback.Rating,
            order.Customer!,
            orderFeedback.CreatedOnUtc
        );
        
        _triggerOutboxRepository.Add(trigger);

        return Result.Success("Feedback successfully !");
    }
}