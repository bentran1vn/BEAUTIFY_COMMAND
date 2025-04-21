namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.ShiftConfigs;

public class UpdateShiftConfigCommandHandler: ICommandHandler<CONTRACT.Services.ShiftConfigs.Commands.UpdateShiftConfigCommand>
{
    private readonly IRepositoryBase<ShiftConfig, Guid> _shiftConfigRepository;
    private readonly IRepositoryBase<Clinic, Guid> _clinicRepository;
    
    
    public UpdateShiftConfigCommandHandler(IRepositoryBase<ShiftConfig, Guid> shiftConfigRepository, IRepositoryBase<Clinic, Guid> clinicRepository)
    {
        _shiftConfigRepository = shiftConfigRepository;
        _clinicRepository = clinicRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.ShiftConfigs.Commands.UpdateShiftConfigCommand request, CancellationToken cancellationToken)
    {
        var clinicExist = await _clinicRepository.FindAll(x => x.Id.Equals(request.ClinicId))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (clinicExist == null)
        {
            return Result.Failure(new Error("404", "Clinic not found"));
        }
        
        if(clinicExist.IsDeleted || !clinicExist.IsActivated || clinicExist.Status != 1)
        {
            return Result.Failure(new Error("500", "Clinic is not activated or deleted"));
        }
        
        var shift = await _shiftConfigRepository.FindByIdAsync(request.Id, cancellationToken);
        
        if (shift == null)
        {
            return Result.Failure(new Error("404", "Shift config not found"));
        }
        
        var overlappingShift = await _shiftConfigRepository
            .FindAll(x => 
                x.Id != request.Id && x.ClinicId == request.ClinicId &&
                (
                    (x.StartTime <= request.StartTime && x.EndTime >= request.EndTime) ||
                    
                    (request.StartTime <= x.StartTime && request.EndTime >= x.EndTime) ||
                    
                    (x.StartTime < request.EndTime && x.StartTime >= request.StartTime) ||
                    
                    (x.EndTime > request.StartTime && x.EndTime <= request.EndTime)
                ))
            .FirstOrDefaultAsync(cancellationToken);
        
        if(overlappingShift != null)
        {
            return Result.Failure(new Error("500", "Shift config overlaps with existing shift"));
        }
        
        shift.StartTime = request.StartTime;
        shift.EndTime = request.EndTime;
        shift.Name = request.Name;
        shift.Note = request.Note;
        
        return Result.Success("Shift config updated successfully");
    }
}