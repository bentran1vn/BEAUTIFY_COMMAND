using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.DoctorServices;
internal sealed class DoctorSetWorkingServiceCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    IRepositoryBase<Service, Guid> serviceRepository,
    IRepositoryBase<DoctorService, Guid> doctorServiceRepository)
    : ICommandHandler<CONTRACT.Services.DoctorServices.Commands.DoctorSetWorkingServiceCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.DoctorServices.Commands.DoctorSetWorkingServiceCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = await userRepository.FindByIdAsync(request.DoctorId, cancellationToken);
        if (doctor is null) throw new UserException.UserNotFoundException(request.DoctorId);

        if (doctor.Role?.Name != Constant.Role.DOCTOR) return Result.Failure(new Error("403", "User is not a doctor"));

        var existingServiceIds = serviceRepository.FindAll(x => request.ServiceIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToHashSet(); // Convert it into HashSet<Guid>


        // Identify missing services
        var missingServiceIds = request.ServiceIds.Except(existingServiceIds).ToList();
        if (missingServiceIds.Count != 0)
        {
            var missingServicesMessage = $"Services not found: {string.Join(", ", missingServiceIds)}";
            return Result.Failure(new Error("404", missingServicesMessage));
        }

        // Prevent duplicate entries
        var existingDoctorServiceIds = doctorServiceRepository
            .FindAll(ds => ds.DoctorId == request.DoctorId && request.ServiceIds.Contains(ds.ServiceId))
            .Select(ds => ds.ServiceId).ToHashSet();


        var newDoctorServices = request.ServiceIds
            .Where(serviceId => !existingDoctorServiceIds.Contains(serviceId))
            .Select(serviceId => new DoctorService
            {
                Id = Guid.NewGuid(),
                DoctorId = request.DoctorId,
                Doctor = doctor,
                ServiceId = serviceId
            })
            .ToList();

        if (newDoctorServices.Count == 0) return Result.Success();
        var doctorService = newDoctorServices.Select(x => new EntityEvent.DoctorServiceEntity
        {
            Id = x.Id,
            ServiceId = x.ServiceId,
            Doctor = new EntityEvent.UserEntity
            {
                Id = x.Doctor.Id,
                FullName = x.Doctor.FirstName + " " + x.Doctor.LastName,
                Email = x.Doctor.Email,
                PhoneNumber = x.Doctor.PhoneNumber,
                ProfilePictureUrl = x.Doctor.ProfilePicture
            }
        }).ToList();
        newDoctorServices.FirstOrDefault().RaiseDoctorServiceCreatedEvent(doctorService);
        doctorServiceRepository.AddRange(newDoctorServices);


        return Result.Success();
    }
}