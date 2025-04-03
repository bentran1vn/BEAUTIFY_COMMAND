namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;

internal sealed class ChangeClinicActivateStatusCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepositoryBase,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ChangeClinicActivateStatusCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.Clinics.Commands.ChangeClinicActivateStatusCommand request,
        CancellationToken cancellationToken)
    {
        // ✅ Ensure the user is a clinic admin
        if (currentUserService.Role != Constant.Role.CLINIC_ADMIN)
        {
            return Result.Failure(new Error("401", "Only clinic admin can change status"));
        }

        var clinic = await clinicRepositoryBase.FindByIdAsync(request.ClinicId, cancellationToken)
            ?? throw new ClinicException.ClinicNotFoundException(request.ClinicId);

        // ✅ Toggle current state
        var targetState = !clinic.IsActivated;

        if (clinic.IsParent == true)
        {
            if (targetState)
            {
                // ✅ Enable parent directly without checking child state
                clinic.IsActivated = true;
            }
            else
            {
                // ✅ Only load child clinics when disabling parent
                var childClinics = await clinicRepositoryBase
                    .FindAll(x => x.ParentId == request.ClinicId && x.IsActivated)
                    .ToListAsync(cancellationToken);

                clinic.IsActivated = false;
                foreach (var childClinic in childClinics)
                {
                    childClinic.IsActivated = false;
                }

                // ✅ Batch update instead of looping
                if (childClinics.Count > 0)
                {
                    clinicRepositoryBase.UpdateRange(childClinics);
                }
            }
        }
        else
        {
            if (targetState)
            {
                if (clinic.ParentId.HasValue)
                {
                    // ✅ Directly check parent state without loading full object
                    var parentIsActive = await clinicRepositoryBase
                        .FindAll(x => x.Id == clinic.ParentId.Value && x.IsActivated)
                        .AnyAsync(cancellationToken);

                    if (!parentIsActive)
                    {
                        return Result.Failure(new Error("403", "Parent clinic must be activated first."));
                    }
                }

                clinic.IsActivated = true;
            }
            else
            {
                clinic.IsActivated = false;
            }
        }

        // ✅ Minimize database calls
        clinicRepositoryBase.Update(clinic);

        return Result.Success();
    }
}
