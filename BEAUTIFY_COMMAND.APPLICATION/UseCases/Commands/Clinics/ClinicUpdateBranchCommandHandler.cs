using BEAUTIFY_COMMAND.DOMAIN.Exceptions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class
    ClinicUpdateBranchCommandHandler(IRepositoryBase<Clinic, Guid> clinicRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicUpdateBranchCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicUpdateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var clinic = await clinicRepository.FindByIdAsync(request.BranchId, cancellationToken) ??
                     throw new ClinicException.ClinicNotFoundException(request.BranchId);
        clinic.Name = request.Name;
        clinic.Address = request.Address;
        clinic.PhoneNumber = request.PhoneNumber;
        clinic.IsActivated = request.IsActivated;
        clinicRepository.Update(clinic);
        return Result.Success();
    }
}