using BEAUTIFY_COMMAND.DOMAIN.Exceptions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
public class ClinicDeleteBranchCommandHandler(IRepositoryBase<Clinic, Guid> clinicRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicDeleteBranchCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicDeleteBranchCommand request,
        CancellationToken cancellationToken)
    {
        var clinic = await clinicRepository.FindByIdAsync(request.BranchId, cancellationToken) ??
                     throw new ClinicException.ClinicNotFoundException(request.BranchId);

        if (clinic.IsParent == true) return Result.Failure(new Error("404", "Cannot delete parent clinic"));

        var parentClinic = await clinicRepository.FindByIdAsync(clinic.ParentId.Value, cancellationToken) ??
                           throw new ClinicException.ClinicNotFoundException(clinic.ParentId.Value);
        parentClinic.TotalBranches--;
        clinicRepository.Remove(clinic);
        clinicRepository.Update(parentClinic);
        return Result.Success();
    }
}