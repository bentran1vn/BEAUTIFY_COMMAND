namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Feedbacks;
public class UpdateFeedbackCommandHandler : ICommandHandler<CONTRACT.Services.Feedbacks.Commands.UpdateFeedbackCommand>
{
    private readonly IRepositoryBase<CustomerSchedule, Guid> _customerScheduleRepository;
    private readonly IMediaService _mediaService;
    private readonly IRepositoryBase<OrderFeedback, Guid> _orderFeedbackRepository;
    private readonly IRepositoryBase<Order, Guid> _orderRepository;
    private readonly IRepositoryBase<Service, Guid> _serviceRepository;
    private readonly IRepositoryBase<Feedback, Guid> _scheduleFeedbackRepository;
    private readonly IRepositoryBase<Staff, Guid> _staffRepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxRepository;

    public UpdateFeedbackCommandHandler(IRepositoryBase<Feedback, Guid> scheduleFeedbackRepository,
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

    // public async Task<Result> Handle(CONTRACT.Services.Feedbacks.Commands.UpdateFeedbackCommand request,
    //     CancellationToken cancellationToken)
    // {
    //     // Validate request ratings
    //     if (request.Rating < 1 || request.Rating > 5)
    //     {
    //         return Result.Failure(new Error("400", "Rating must be between 1 and 5"));
    //     }
    //
    //     foreach (var feedback1 in request.ScheduleFeedbacks)
    //     {
    //         if (feedback1.Rating < 1 || feedback1.Rating > 5)
    //         {
    //             return Result.Failure(new Error("400", "Schedule feedback rating must be between 1 and 5"));
    //         }
    //     }
    //     
    //     var feedback = await _orderFeedbackRepository
    //         .FindAll(x => x.Id.Equals(request.FeedbackId) && !x.IsDeleted)
    //         .FirstOrDefaultAsync(cancellationToken);
    //
    //     var order = await _orderRepository
    //         .FindSingleAsync(x => x.OrderFeedbackId.Equals(request.FeedbackId) && !x.IsDeleted, cancellationToken);
    //
    //     if (feedback is null) return Result.Failure(new Error("404", "Feedback not found"));
    //     
    //     // Get old feedback rating for calculations
    //     var oldOrderRating = feedback.Rating;
    //
    //     var scheduleIds = request.ScheduleFeedbacks.Select(x => x.CustomerScheduleId).ToList();
    //
    //     var customerSchedule = await _customerScheduleRepository
    //         .FindAll(x => scheduleIds.Contains(x.Id))
    //         .Include(x => x.Doctor)
    //         .ThenInclude(y => y.User)
    //         .Include(x => x.Feedback)
    //         .ToListAsync(cancellationToken);
    //
    //     // if (customerSchedule == null || !customerSchedule.Any())
    //     // {
    //     //     return Result.Failure(new Error("404", "Customer schedule not found"));
    //     // }
    //
    //     // if(customerSchedule.Any(x => x.Status != Constant.OrderStatus.ORDER_COMPLETED))
    //     // {
    //     //     return Result.Failure(new Error("400", "Customer schedule not completed"));
    //     // }
    //
    //     if (customerSchedule.Count != request.ScheduleFeedbacks.Count)
    //         return Result.Failure(new Error("500", "Missing schedule feedback"));
    //
    //     Dictionary<Guid, (int Sum, int Count)> doctorRatings = new();
    //
    //     var feedbacks = request.ScheduleFeedbacks.Select(x =>
    //     {
    //         var schedule = customerSchedule.FirstOrDefault(y => y.Id.Equals(x.CustomerScheduleId));
    //
    //         if (schedule != null)
    //         {
    //             if (doctorRatings.TryGetValue(schedule.Doctor!.User.Id, out var current))
    //                 doctorRatings[schedule.Doctor!.User.Id] = (current.Sum + x.Rating, current.Count + 1);
    //             else
    //                 doctorRatings[schedule.Doctor!.User.Id] = (x.Rating, 1);
    //         }
    //
    //         return new Feedback
    //         {
    //             Id = Guid.NewGuid(),
    //             Content = x.Content,
    //             Rating = x.Rating,
    //             CustomerScheduleId = x.CustomerScheduleId
    //         };
    //     }).ToList();
    //
    //     var removeFeedbacks = customerSchedule.Select(x => x.Feedback).ToList();
    //
    //     if (removeFeedbacks.Any()) _scheduleFeedbackRepository.RemoveMultiple(removeFeedbacks!);
    //
    //     _scheduleFeedbackRepository.AddRange(feedbacks);
    //
    //     var normalizedRatings = doctorRatings.ToDictionary(
    //         pair => pair.Key,
    //         pair => Math.Clamp((int)Math.Round((double)pair.Value.Sum / pair.Value.Count), 1, 5)
    //     );
    //
    //     var staff = await _staffRepository.FindAll(x => !x.IsDeleted &&
    //                                                     x.Role.Name == Constant.Role.DOCTOR &&
    //                                                     normalizedRatings.Keys.Contains(x.Id)
    //     ).ToListAsync(cancellationToken);
    //
    //     if (staff == null || !staff.Any()) return Result.Failure(new Error("404", "Staff not found"));
    //
    //     // Update staff ratings
    //     foreach (var staffMember in staff)
    //         if (normalizedRatings.TryGetValue(staffMember.Id, out var rating))
    //         {
    //             var doctorRating = staffMember.Rating;
    //             var oldRating = customerSchedule
    //                 .Where(x => x.DoctorId.Equals(staffMember.Id))
    //                 .Select(x => x.Feedback.Rating)
    //                 .FirstOrDefault();
    //
    //             var defaultRating = 2 * doctorRating - oldRating;
    //
    //             staffMember.Rating = (defaultRating + rating) / 2;
    //         }
    //
    //     _staffRepository.AddRange(staff);
    //
    //     var servicesCoverImageTasks = request.Images.Select(_mediaService.UploadImageAsync);
    //
    //     var coverImageUrls = await Task.WhenAll(servicesCoverImageTasks);
    //
    //     feedback.Rating = request.Rating;
    //     feedback.Content = request.Content;
    //
    //     _orderFeedbackRepository.Update(feedback);
    //
    //     var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
    //
    //     var trigger = TriggerOutbox.UpdateFeedbackEvent(
    //         request.FeedbackId, (Guid)order.ServiceId,
    //         coverImageUrls, request.Content, request.Rating,
    //         TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone)
    //     );
    //
    //     _triggerOutboxRepository.Add(trigger);
    //
    //     return Result.Success("Update Feedback successfully !");
    // }
    
    public async Task<Result> Handle(CONTRACT.Services.Feedbacks.Commands.UpdateFeedbackCommand request,
    CancellationToken cancellationToken)
    {
        // Validate request ratings
        if (request.Rating < 1 || request.Rating > 5)
        {
            return Result.Failure(new Error("400", "Rating must be between 1 and 5"));
        }
        
        foreach (var feedback1 in request.ScheduleFeedbacks)
        {
            if (feedback1.Rating < 1 || feedback1.Rating > 5)
            {
                return Result.Failure(new Error("400", "Schedule feedback rating must be between 1 and 5"));
            }
        }

        var feedback = await _orderFeedbackRepository
            .FindAll(x => x.Id.Equals(request.FeedbackId) && !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        var order = await _orderRepository
            .FindSingleAsync(x => x.OrderFeedbackId.Equals(request.FeedbackId) && !x.IsDeleted, cancellationToken);

        if (feedback is null) return Result.Failure(new Error("404", "Feedback not found"));

        // Get old feedback rating for calculations
        var oldOrderRating = feedback.Rating;

        var scheduleIds = request.ScheduleFeedbacks.Select(x => x.CustomerScheduleId).ToList();

        var customerSchedule = await _customerScheduleRepository
            .FindAll(x => scheduleIds.Contains(x.Id))
            .Include(x => x.Doctor)
            .ThenInclude(y => y.User)
            .Include(x => x.Feedback)
            .ToListAsync(cancellationToken);

        if (customerSchedule.Count != request.ScheduleFeedbacks.Count)
            return Result.Failure(new Error("500", "Missing schedule feedback"));

        // Create a map of doctor IDs to their old feedback ratings
        var doctorOldRatings = new Dictionary<Guid, List<double>>();
        foreach (var schedule in customerSchedule)
        {
            if (schedule.Doctor != null && schedule.Feedback != null)
            {
                var doctorId = schedule.Doctor.User.Id;
                if (!doctorOldRatings.ContainsKey(doctorId))
                    doctorOldRatings[doctorId] = new List<double>();
                    
                doctorOldRatings[doctorId].Add(schedule.Feedback.Rating);
            }
        }

        // Track new ratings
        Dictionary<Guid, (int Sum, int Count)> doctorNewRatings = new();

        // Create new feedback objects and calculate new ratings
        var newFeedbacks = request.ScheduleFeedbacks.Select(x =>
        {
            var schedule = customerSchedule.FirstOrDefault(y => y.Id.Equals(x.CustomerScheduleId));

            if (schedule != null)
            {
                var doctorId = schedule.Doctor!.User.Id;
                if (doctorNewRatings.TryGetValue(doctorId, out var current))
                    doctorNewRatings[doctorId] = (current.Sum + x.Rating, current.Count + 1);
                else
                    doctorNewRatings[doctorId] = (x.Rating, 1);
            }

            var newFeedback = new Feedback
            {
                Id = Guid.NewGuid(),
                Content = x.Content,
                Rating = x.Rating,
                CustomerScheduleId = x.CustomerScheduleId
            };
            
            // Update the reference in CustomerSchedule
            if (schedule != null)
            {
                schedule.FeedbackId = newFeedback.Id;
            }
            
            return newFeedback;
        }).ToList();

        // Remove old feedbacks
        var oldFeedbacks = customerSchedule.Select(x => x.Feedback).Where(f => f != null).ToList();
        if (oldFeedbacks.Any()) _scheduleFeedbackRepository.RemoveMultiple(oldFeedbacks!);

        // Add new feedbacks
        _scheduleFeedbackRepository.AddRange(newFeedbacks);
        
        // Update customer schedules with new feedback IDs
        _customerScheduleRepository.UpdateRange(customerSchedule);

        // Normalize ratings for easier calculation
        var normalizedNewRatings = doctorNewRatings.ToDictionary(
            pair => pair.Key,
            pair => Math.Clamp((int)Math.Round((double)pair.Value.Sum / pair.Value.Count), 1, 5)
        );

        // Get staff members
        var staff = await _staffRepository.FindAll(
            x => !x.IsDeleted && 
                 x.Role.Name == Constant.Role.DOCTOR &&
                 normalizedNewRatings.Keys.Contains(x.Id)
        ).ToListAsync(cancellationToken);

        if (staff == null || !staff.Any()) return Result.Failure(new Error("404", "Staff not found"));

        // Get total previous ratings for these doctors
        var staffFeedback = await _customerScheduleRepository.FindAll(
            x => !x.IsDeleted &&
                 normalizedNewRatings.Keys.Contains(x.DoctorId) &&
                 x.FeedbackId != null &&
                 !scheduleIds.Contains(x.Id) // Exclude current feedback schedules
        ).AsNoTracking().CountAsync(cancellationToken);

        // Update staff ratings
        staff = staff.Select(x =>
        {
            if (normalizedNewRatings.TryGetValue(x.Id, out var newRating))
            {
                // Calculate average old rating for this doctor
                var oldAvgRating = 0;
                if (doctorOldRatings.TryGetValue(x.Id, out var oldRatings) && oldRatings.Any())
                    oldAvgRating = (int)Math.Round(oldRatings.Average());
                    
                // Get count of previous ratings for this doctor
                var previousRatingCount = staffFeedback;
                
                // If there were previous ratings, recalculate
                if (previousRatingCount > 0)
                {
                    // Remove effect of old ratings, add effect of new ratings
                    var weightedCurrentRating = x.Rating * previousRatingCount;
                    var adjustedRating = weightedCurrentRating - oldAvgRating + newRating;
                    x.Rating = adjustedRating / previousRatingCount;
                }
                else
                {
                    // If no previous ratings, just use the new rating
                    x.Rating = newRating;
                }
            }
            return x;
        }).ToList();

        _staffRepository.UpdateRange(staff);

        // Update service rating
        var currentService = await _serviceRepository.FindAll(
            x => !x.IsDeleted && x.Id == order.ServiceId
        ).FirstOrDefaultAsync(cancellationToken);

        if (currentService != null)
        {
            var previousOrderRatingCount = await _orderRepository.FindAll(
                x => !x.IsDeleted
                     && x.ServiceId == order.ServiceId
                     && x.OrderFeedbackId != null
                     && x.OrderFeedbackId != feedback.Id // Exclude current feedback
            ).AsNoTracking().CountAsync(cancellationToken);
            
            if (previousOrderRatingCount > 0)
            {
                // Remove effect of old rating, add effect of new rating
                var totalRating = currentService.Rating * (previousOrderRatingCount + 1);
                totalRating = totalRating - oldOrderRating + request.Rating;
                currentService.Rating = totalRating / (previousOrderRatingCount + 1);
            }
            else
            {
                currentService.Rating = request.Rating;
            }
            
            _serviceRepository.Update(currentService);
        }

        // Upload new images
        var servicesCoverImageTasks = request.Images.Select(_mediaService.UploadImageAsync);
        var coverImageUrls = await Task.WhenAll(servicesCoverImageTasks);

        // Update feedback
        feedback.Rating = request.Rating;
        feedback.Content = request.Content;
        _orderFeedbackRepository.Update(feedback);

        // Create outbox message
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var trigger = TriggerOutbox.UpdateFeedbackEvent(
            request.FeedbackId, 
            (Guid)order.ServiceId,
            coverImageUrls, 
            request.Content, 
            request.Rating,
            TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone),
            currentService?.Rating ?? request.Rating // Pass updated service rating
        );

        _triggerOutboxRepository.Add(trigger);

        return Result.Success("Update Feedback successfully!");
    }
}