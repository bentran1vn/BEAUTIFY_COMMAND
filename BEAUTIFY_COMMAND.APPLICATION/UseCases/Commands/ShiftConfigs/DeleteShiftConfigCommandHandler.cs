namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.ShiftConfigs;

public class DeleteShiftConfigCommandHandler: ICommandHandler<CONTRACT.Services.ShiftConfigs.Commands.DeleteShiftConfigCommand>
{
    private readonly IRepositoryBase<ShiftConfig, Guid> _shiftConfigRepository;

    public DeleteShiftConfigCommandHandler(IRepositoryBase<ShiftConfig, Guid> shiftConfigRepository)
    {
        _shiftConfigRepository = shiftConfigRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.ShiftConfigs.Commands.DeleteShiftConfigCommand request, CancellationToken cancellationToken)
    {
        var shift = await _shiftConfigRepository.FindByIdAsync(request.Id, cancellationToken);
        
        if (shift == null || shift.IsDeleted)
        {
            return Result.Failure(new Error("404", "Shift config not found"));
        }
        
        if(shift.ClinicId != request.ClinicId)
        {
            return Result.Failure(new Error("403", "Shift config not belong to this clinic"));
        }
        
        _shiftConfigRepository.Remove(shift);

        return Result.Success("Shift config deleted successfully");
    }
}