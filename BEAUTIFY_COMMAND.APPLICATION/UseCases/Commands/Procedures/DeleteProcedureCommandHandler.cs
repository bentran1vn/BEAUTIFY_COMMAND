namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Procedures;
public class
    DeleteProcedureCommandHandler : ICommandHandler<CONTRACT.Services.Procedures.Commands.DeleteProcedureCommand>
{
    private readonly IRepositoryBase<Procedure, Guid> _procedureRepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxRepository;

    public DeleteProcedureCommandHandler(IRepositoryBase<Procedure, Guid> procedureRepository, IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository)
    {
        _procedureRepository = procedureRepository;
        _triggerOutboxRepository = triggerOutboxRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Procedures.Commands.DeleteProcedureCommand request,
        CancellationToken cancellationToken)
    {
        var isExisted = await _procedureRepository.FindByIdAsync(request.Id, cancellationToken);

        if (isExisted == null || isExisted.IsDeleted) return Result.Failure(new Error("404", "Procedure not found "));

        _procedureRepository.Remove(isExisted);
        
        var trigger = TriggerOutbox.RaiseDeleteServiceProcedureEvent(
            (Guid)isExisted.ServiceId!, isExisted.Id);
        
        _triggerOutboxRepository.Add(trigger);

        return Result.Success("Procedure deleted");
    }
}