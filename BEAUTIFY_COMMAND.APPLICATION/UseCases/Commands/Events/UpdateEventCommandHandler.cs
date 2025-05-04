namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Events;

public class UpdateEventCommandHandler : ICommandHandler<CONTRACT.Services.Events.Commands.UpdateEvent>
{
    private readonly IRepositoryBase<Event, Guid> _eventRepository;
    private readonly IMediaService _mediaService;

    public UpdateEventCommandHandler(IRepositoryBase<Event, Guid> eventRepository, IMediaService mediaService)
    {
        _eventRepository = eventRepository;
        _mediaService = mediaService;
    }

    public async Task<Result> Handle(CONTRACT.Services.Events.Commands.UpdateEvent request, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.FindByIdAsync(request.Id, cancellationToken);
        
        if (eventEntity is null || eventEntity.IsDeleted)
            return Result.Failure(new Error("404", "Event not found"));
        
        eventEntity.Name = request.Name;
        eventEntity.Description = request.Description;
        eventEntity.StartDate = request.StartDate;
        eventEntity.EndDate = request.EndDate;
        eventEntity.Date = request.Date;
        eventEntity.ClinicId = request.ClinicId;
        eventEntity.Image = request.Image != null ? await _mediaService.UploadImageAsync(request.Image) : eventEntity.Image;
        
        return Result.Success("Event updated successfully");
    }
}