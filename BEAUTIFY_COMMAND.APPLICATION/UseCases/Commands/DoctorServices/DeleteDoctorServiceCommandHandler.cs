using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.DoctorServices;
internal sealed class DeleteDoctorServiceCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    IRepositoryBase<Service, Guid> serviceRepository,
    IRepositoryBase<DoctorService, Guid> doctorServiceRepository)
    : ICommandHandler<CONTRACT.Services.DoctorServices.Commands.DeleteDoctorServiceCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.DoctorServices.Commands.DeleteDoctorServiceCommand request,
        CancellationToken cancellationToken)
    {
        // Validate doctor existence
        var doctor = await userRepository.FindByIdAsync(request.DoctorId, cancellationToken);
        if (doctor is null)
        {
            throw new UserException.UserNotFoundException(request.DoctorId);
        }

        // Ensure user is a doctor
        if (doctor.Role?.Name != Constant.DOCTOR)
        {
            return Result.Failure(new Error("403", "User is not a doctor"));
        }

        // Fetch existing service IDs in a single query
        var existingServiceIds = serviceRepository
            .FindAll(x => request.ServiceIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToHashSet();

        // Identify missing services
        var missingServiceIds = request.ServiceIds.Except(existingServiceIds).ToList();
        if (missingServiceIds.Count > 0)
        {
            var missingServicesMessage = $"Services not found: {string.Join(", ", missingServiceIds)}";
            return Result.Failure(new Error("404", missingServicesMessage));
        }

        // Retrieve doctor services that need to be deleted
        var doctorServicesToDelete = doctorServiceRepository
            .FindAll(ds => ds.DoctorId == request.DoctorId && request.ServiceIds.Contains(ds.ServiceId))
            .ToList();

        if (doctorServicesToDelete.Count == 0)
        {
            return Result.Failure(new Error("404", "No matching doctor services found for deletion"));
        }

        // Delete the records
        doctorServiceRepository.RemoveMultiple(doctorServicesToDelete);
        doctorServicesToDelete[0]
            .RaiseDoctorServiceDeletedEvent(doctorServicesToDelete.Select(x => x.ServiceId).ToList());
        return Result.Success();
    }
}