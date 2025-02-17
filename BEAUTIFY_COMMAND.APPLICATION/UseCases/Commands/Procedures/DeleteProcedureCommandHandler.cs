namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Procedures;

public class DeleteProcedureCommandHandler: ICommandHandler<CONTRACT.Services.Procedures.Commands.DeleteProcedureCommand>
{
    private readonly IRepositoryBase<Procedure, Guid> _procedureRepository;

    public DeleteProcedureCommandHandler(IRepositoryBase<Procedure, Guid> procedureRepository)
    {
        _procedureRepository = procedureRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Procedures.Commands.DeleteProcedureCommand request, CancellationToken cancellationToken)
    {
        var isExisted = await _procedureRepository.FindByIdAsync(request.Id, cancellationToken);
        
        if (isExisted == null || isExisted.IsDeleted)
        {
            return Result.Failure(new Error("404", "Procedure not found "));
        }
        
        _procedureRepository.Remove(isExisted);
        
        return Result.Success("Procedure deleted");
    }
}