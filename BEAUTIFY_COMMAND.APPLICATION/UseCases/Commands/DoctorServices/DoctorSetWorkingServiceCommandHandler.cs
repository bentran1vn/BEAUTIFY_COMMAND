using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.DoctorServices;
internal sealed class DoctorSetWorkingServiceCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IRepositoryBase<Service, Guid> serviceRepository,
    IRepositoryBase<DoctorService, Guid> doctorServiceRepository)
    : ICommandHandler<CONTRACT.Services.DoctorServices.Commands.DoctorSetWorkingServiceCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.DoctorServices.Commands.DoctorSetWorkingServiceCommand request,
        CancellationToken cancellationToken)
    {
        // Validate service exists first (quick check)
        var service = await serviceRepository.FindByIdAsync(request.ServiceIds, cancellationToken);
        if (service is null)
            return Result.Failure(new Error("404", $"Service not found: {request.ServiceIds}"));

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
            return Result.Failure(new Error("409",
                $"These doctors already have this service: {string.Join(", ", names)}"));
        }

        // Get doctors with role information in a single query
        var doctors = await staffRepository.FindAll(x => request.DoctorId.Contains(x.Id))
            .Include(x => x.Role)
            .ToListAsync(cancellationToken);

        if (doctors.Count != request.DoctorId.Count)
        {
            return Result.Failure(new Error("404", $"One or more doctors not found"));
        }

        // Check if all users are doctors
        var nonDoctors = doctors.Where(d => d?.Role?.Name != Constant.Role.DOCTOR).ToList();
        if (nonDoctors.Count != 0)
        {
            var nonDoctorId = nonDoctors.First().Id;
            return Result.Failure(new Error("403", $"User {nonDoctorId} is not a doctor"));
        }

        // Create new doctor-service relationships
        var newDoctorServices = request.DoctorId
            .Select(doctorId => new DoctorService
            {
                Id = Guid.NewGuid(),
                DoctorId = doctorId,
                ServiceId = request.ServiceIds
            }).ToList();

        if (newDoctorServices.Count == 0)
        {
            return Result.Success("No new doctor-service relationships to create");
        }

        // Create doctor service entities for the event
        var doctorServiceEntities = new List<EntityEvent.DoctorServiceEntity>();
        foreach (var ds in newDoctorServices)
        {
            var doctor = doctors.FirstOrDefault(d => d.Id == ds.DoctorId);
            if (doctor != null)
            {
                doctorServiceEntities.Add(new EntityEvent.DoctorServiceEntity
                {
                    Id = ds.Id,
                    ServiceId = ds.ServiceId,
                    Doctor = new EntityEvent.UserEntity
                    {
                        Id = doctor.Id,
                        FullName = $"{doctor.FirstName} {doctor.LastName}",
                        Email = doctor.Email,
                        PhoneNumber = doctor.PhoneNumber ?? "",
                        ProfilePictureUrl = doctor.ProfilePicture ?? ""
                    }
                });
            }
        }

        // Raise event and save changes
        if (doctorServiceEntities.Count <= 0) return Result.Success("No valid doctor-service relationships to create");
        newDoctorServices.First().RaiseDoctorServiceCreatedEvent(doctorServiceEntities);
        doctorServiceRepository.AddRange(newDoctorServices);
        return Result.Success("Services assigned to doctors successfully");

    }
}