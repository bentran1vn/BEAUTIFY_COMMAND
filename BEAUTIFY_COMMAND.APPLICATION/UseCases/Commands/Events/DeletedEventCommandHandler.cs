namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Events;

public class DeletedEventCommandHandler: ICommandHandler<CONTRACT.Services.Events.Commands.DeleteEventCommand>
{
    private readonly IRepositoryBase<Event, Guid> _eventRepository;

    public DeletedEventCommandHandler(IRepositoryBase<Event, Guid> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Events.Commands.DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _eventRepository.FindByIdAsync(request.Id, cancellationToken);
        if (eventEntity is null || eventEntity.IsDeleted)
            return Result.Failure(new Error("404", "Event not found"));
        
        _eventRepository.Remove(eventEntity);
        
        return Result.Success("Event deleted successfully");
    }
}