using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;
using Microsoft.EntityFrameworkCore;

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
        var doctor = await staffRepository.FindByIdAsync(request.DoctorId, cancellationToken);
        if (doctor is null) throw new UserException.UserNotFoundException(request.DoctorId);

        if (doctor.Role?.Name != Constant.Role.DOCTOR) return Result.Failure(new Error("403", "User is not a doctor"));

        var existingServices = serviceRepository.FindAll(x => request.ServiceIds.Contains(x.Id)).ToList();
        var existingServiceIds = existingServices.Select(x => x.Id).ToHashSet();
        var missingServiceIds = request.ServiceIds.Except(existingServiceIds).ToList();
        if (missingServiceIds.Count != 0)
        {
            var missingServicesMessage = $"Services not found: {string.Join(", ", missingServiceIds)}";
            return Result.Failure(new Error("404", missingServicesMessage));
        }
        
        

        var existingDoctorServices = doctorServiceRepository
            .FindAll(ds => ds.DoctorId == request.DoctorId && request.ServiceIds.Contains(ds.ServiceId))
            .Include(ds => ds.Service)
            .ToList();

        var existingDoctorServiceIds = existingDoctorServices.Select(ds => ds.ServiceId).ToHashSet();
        var existingServiceNames =
            existingDoctorServices.Select(ds => ds.Service?.Name).Where(name => name != null).ToList();

        if (existingServiceNames.Count > 0)
        {
            var duplicateServicesMessage =
                $"Doctor already has these services: {string.Join(", ", existingServiceNames)}";
            return Result.Failure(new Error("409", duplicateServicesMessage));
        }

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