using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.DoctorServices;
internal sealed class DoctorSetWorkingServiceCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IRepositoryBase<Service, Guid> serviceRepository,
    IRepositoryBase<DoctorService, Guid> doctorServiceRepository)
    : ICommandHandler<CONTRACT.Services.DoctorServices.Commands.DoctorSetWorkingServiceCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.DoctorServices.Commands.DoctorSetWorkingServiceCommand request, CancellationToken cancellationToken)
    {
        var doctors = await staffRepository.FindAll(x => request.DoctorId.Contains(x.Id)).ToListAsync(cancellationToken);
        
        if (doctors.Count != request.DoctorId.Count) {
            return Result.Failure(new Error("404", $"Doctor not found"));
        }
        
        if (doctors.Any(d => d?.Role?.Name != Constant.Role.DOCTOR))
            return Result.Failure(new Error("403", $"User {request.DoctorId.First(id => doctors[Array.IndexOf(request.DoctorId.ToArray(), id)]?.Role?.Name != Constant.Role.DOCTOR)} is not a doctor"));
        
        var service = await serviceRepository.FindByIdAsync(request.ServiceIds, cancellationToken);
        if (service is null) return Result.Failure(new Error("404", $"Service not found: {request.ServiceIds}"));
        
        var existingDoctorServices = await doctorServiceRepository
            .FindAll(ds => request.DoctorId.Contains(ds.DoctorId) && ds.ServiceId == request.ServiceIds)
            .Include(ds => ds.Doctor)
            .ToListAsync(cancellationToken);

        if (existingDoctorServices.Count > 0)
        {
            var names = existingDoctorServices
                .Where(ds => ds.Doctor != null)
                .Select(ds => $"{ds.Doctor.FirstName} {ds.Doctor.LastName}")
                .ToList();
            return Result.Failure(new Error("409", $"These doctors already have this service: {string.Join(", ", names)}"));
        }

        var newDoctorServices = request.DoctorId
            .Select(doctorId => new DoctorService
            {
                Id = Guid.NewGuid(),
                DoctorId = doctorId,
                Doctor = doctors.First(d => d?.Id == doctorId),
                ServiceId = request.ServiceIds
            }).ToList();
        if (newDoctorServices.Count == 0) return Result.Success();

        var doctorServiceEntities = newDoctorServices.Select(x => new EntityEvent.DoctorServiceEntity
        {
            Id = x.Id,
            ServiceId = x.ServiceId,
            Doctor = new EntityEvent.UserEntity
            {
                Id = x.Doctor.Id,
                FullName = $"{x.Doctor.FirstName} {x.Doctor.LastName}",
                Email = x.Doctor.Email,
                PhoneNumber = x.Doctor.PhoneNumber,
                ProfilePictureUrl = x.Doctor.ProfilePicture
            }
        }).ToList();

        if (newDoctorServices.Any())
        {
            newDoctorServices.First().RaiseDoctorServiceCreatedEvent(doctorServiceEntities);
        }
        doctorServiceRepository.AddRange(newDoctorServices);

        return Result.Success("Services assigned to doctors successfully");
    }
}