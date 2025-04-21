namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.ShiftConfigs;

public class CreateShiftConfigCommandHandler : ICommandHandler<CONTRACT.Services.ShiftConfigs.Commands.CreateShiftConfigCommand>
{
    private readonly IRepositoryBase<ShiftConfig, Guid> _shiftConfigRepository;
    private readonly IRepositoryBase<Clinic, Guid> _clinicRepository;

    public CreateShiftConfigCommandHandler(IRepositoryBase<ShiftConfig, Guid> shiftConfigRepository, IRepositoryBase<Clinic, Guid> clinicRepository)
    {
        _shiftConfigRepository = shiftConfigRepository;
        _clinicRepository = clinicRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.ShiftConfigs.Commands.CreateShiftConfigCommand request, CancellationToken cancellationToken)
    {
        var clinicExist = await _clinicRepository.FindSingleAsync(x => x.Id.Equals(request.ClinicId), cancellationToken);
        
        if (clinicExist == null)
        {
            return Result.Failure(new Error("404", "Clinic not found"));
        }
        
        if(clinicExist.IsDeleted || !clinicExist.IsActivated || clinicExist.Status != 1)
        {
            return Result.Failure(new Error("500", "Clinic is not activated or deleted"));
        }
        
        var exist = await _shiftConfigRepository
            .FindAll(x => x.ClinicId == request.ClinicId && x.StartTime == request.StartTime && x.EndTime == request.EndTime)
            .FirstOrDefaultAsync(cancellationToken);

        if (exist != null)
        {
            return Result.Failure(new Error("500", "Shift config already exists"));
        }

        // var overlappingShift = await _shiftConfigRepository
        //     .FindAll(x => 
        //         x.ClinicId == request.ClinicId && 
        //         (
        //             (x.StartTime <= request.StartTime && x.EndTime >= request.EndTime) ||
        //             
        //             (request.StartTime <= x.StartTime && request.EndTime >= x.EndTime) ||
        //             
        //             (x.StartTime < request.EndTime && x.StartTime >= request.StartTime) ||
        //             
        //             (x.EndTime > request.StartTime && x.EndTime <= request.EndTime)
        //         ))
        //     .FirstOrDefaultAsync(cancellationToken);
        //
        // if(overlappingShift != null)
        // {
        //     return Result.Failure(new Error("500", "Shift config overlaps with existing shift"));
        // }
        
        var shiftConfig = new ShiftConfig
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Note = request.Note,
            ClinicId = request.ClinicId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
        };
        
        _shiftConfigRepository.Add(shiftConfig);

        return Result.Success("Shift Config created successfully");
    }
}