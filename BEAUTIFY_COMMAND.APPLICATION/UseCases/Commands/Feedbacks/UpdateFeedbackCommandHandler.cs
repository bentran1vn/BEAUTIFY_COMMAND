namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Feedbacks;

public class UpdateFeedbackCommandHandler: ICommandHandler<CONTRACT.Services.Feedbacks.Commands.UpdateFeedbackCommand>
{
    private readonly IRepositoryBase<Feedback, Guid> _scheduleFeedbackRepository;
    private readonly IRepositoryBase<OrderFeedback, Guid> _orderFeedbackRepository;
    private readonly IRepositoryBase<Order, Guid> _orderRepository;
    private readonly IRepositoryBase<CustomerSchedule, Guid> _customerScheduleRepository;
    private readonly IRepositoryBase<Staff, Guid> _staffRepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxRepository;
    private readonly IMediaService _mediaService;

    public UpdateFeedbackCommandHandler(IRepositoryBase<Feedback, Guid> scheduleFeedbackRepository, IRepositoryBase<OrderFeedback, Guid> orderFeedbackRepository, IRepositoryBase<Order, Guid> orderRepository, IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepository, IRepositoryBase<Staff, Guid> staffRepository, IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository, IMediaService mediaService)
    {
        _scheduleFeedbackRepository = scheduleFeedbackRepository;
        _orderFeedbackRepository = orderFeedbackRepository;
        _orderRepository = orderRepository;
        _customerScheduleRepository = customerScheduleRepository;
        _staffRepository = staffRepository;
        _triggerOutboxRepository = triggerOutboxRepository;
        _mediaService = mediaService;
    }

    public async Task<Result> Handle(CONTRACT.Services.Feedbacks.Commands.UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await _orderFeedbackRepository
            .FindAll(x => x.Id.Equals(request.FeedbackId) && !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        var order = await _orderRepository
            .FindSingleAsync(x => x.OrderFeedbackId.Equals(request.FeedbackId) && !x.IsDeleted, cancellationToken);

        if (feedback is null)
        {
            return Result.Failure(new Error("404", "Feedback not found"));
        }
        
        var scheduleIds = request.ScheduleFeedbacks.Select(x => x.CustomerScheduleId).ToList();
        
        var customerSchedule = await _customerScheduleRepository
            .FindAll(x => scheduleIds.Contains(x.Id))
            .Include(x => x.Doctor)
            .ThenInclude(y => y.User)
            .Include(x => x.Feedback)
            .ToListAsync(cancellationToken);
        
        // if (customerSchedule == null || !customerSchedule.Any())
        // {
        //     return Result.Failure(new Error("404", "Customer schedule not found"));
        // }
        
        // if(customerSchedule.Any(x => x.Status != Constant.OrderStatus.ORDER_COMPLETED))
        // {
        //     return Result.Failure(new Error("400", "Customer schedule not completed"));
        // }

        if (customerSchedule.Count != request.ScheduleFeedbacks.Count)
        {
            return Result.Failure(new Error("500", "Missing schedule feedback"));
        }
        
        Dictionary<Guid, (int Sum, int Count)> doctorRatings = new Dictionary<Guid, (int, int)>();

        var feedbacks = request.ScheduleFeedbacks.Select(x =>
        {
            var schedule = customerSchedule.FirstOrDefault(y => y.Id.Equals(x.CustomerScheduleId));
            
            if (schedule != null)
            {
                if (doctorRatings.TryGetValue(schedule.Doctor!.User.Id, out var current))
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
                CustomerScheduleId = x.CustomerScheduleId
            };
        }).ToList();
        
        var removeFeedbacks = customerSchedule.Select(x => x.Feedback).ToList();
        
        if (removeFeedbacks.Any())
        {
            _scheduleFeedbackRepository.RemoveMultiple(removeFeedbacks!);
        }
        
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
            return Result.Failure(new Error("404", "Staff not found"));
        }
        
        // Update staff ratings
        foreach (var staffMember in staff)
        {
            if (normalizedRatings.TryGetValue(staffMember.Id, out var rating))
            {
                var doctorRating = staffMember.Rating;
                var oldRating = customerSchedule
                    .Where(x => x.DoctorId.Equals(staffMember.Id))
                    .Select(x => x.Feedback.Rating)
                    .FirstOrDefault();
                
                var defaultRating = 2 * doctorRating - oldRating;
                
                staffMember.Rating = (defaultRating + rating) / 2;
            }
        }
        
        _staffRepository.AddRange(staff);
        
        var servicesCoverImageTasks = request.Images.Select(_mediaService.UploadImageAsync);

        var coverImageUrls = await Task.WhenAll(servicesCoverImageTasks);

        feedback.Rating = request.Rating;
        feedback.Content = request.Content;
        
        _orderFeedbackRepository.Update(feedback);
        
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        
        var trigger = TriggerOutbox.UpdateFeedbackEvent(
            request.FeedbackId, (Guid)order.ServiceId,
            coverImageUrls, request.Content, request.Rating,
            TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone)
        );
        
        _triggerOutboxRepository.Add(trigger);
        
        return Result.Success("Update Feedback successfully !");
    }
}