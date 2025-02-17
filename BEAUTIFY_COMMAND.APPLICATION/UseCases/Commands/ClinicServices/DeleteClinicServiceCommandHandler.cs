namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.ClinicServices;

public class DeleteClinicServiceCommandHandler: ICommandHandler<CONTRACT.Services.ClinicSerivices.Commands.DeleteClinicServiceCommand>
{
    private readonly IRepositoryBase<Service, Guid> _clinicServiceRepository;

    public DeleteClinicServiceCommandHandler(IRepositoryBase<Service, Guid> clinicServiceRepository)
    {
        _clinicServiceRepository = clinicServiceRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.ClinicSerivices.Commands.DeleteClinicServiceCommand request, CancellationToken cancellationToken)
    {
        var isExisted = await _clinicServiceRepository.FindByIdAsync(request.Id, cancellationToken);
        
        if (isExisted == null || isExisted.IsDeleted)
        {
            return Result.Failure(new Error("404", "Service not found "));
        }
        
        _clinicServiceRepository.Remove(isExisted);
        
        return Result.Success("Clinic service deleted");
    }
}