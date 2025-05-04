namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Events;

public class CreateEventCommandHandler : ICommandHandler<CONTRACT.Services.Events.Commands.CreateEvent>
{
    private readonly IRepositoryBase<Event, Guid> _eventRepository;
    private readonly IMediaService _mediaService;
    private readonly IRepositoryBase<Clinic, Guid> _clinicRepository;

    public CreateEventCommandHandler(IRepositoryBase<Event, Guid> eventRepository, IMediaService mediaService, IRepositoryBase<Clinic, Guid> clinicRepository)
    {
        _eventRepository = eventRepository;
        _mediaService = mediaService;
        _clinicRepository = clinicRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Events.Commands.CreateEvent request, CancellationToken cancellationToken)
    {
        var clinic = await _clinicRepository.FindByIdAsync(request.ClinicId, cancellationToken);
        
        if (clinic is null)
            return Result.Failure(new Error("404", "Clinic not found"));
        
        var eventEntity = new Event()
        {
            Id = Guid.NewGuid(),
            ClinicId = request.ClinicId,
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Date = request.Date,
        };
        
        var imageUrl = await _mediaService.UploadImageAsync(request.Image);
        eventEntity.Image = imageUrl;
        
        _eventRepository.Add(eventEntity);
        
        return Result.Success("Event created successfully");
    }
}