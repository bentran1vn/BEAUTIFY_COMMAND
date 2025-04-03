using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.ClinicServices;
public class
    UpdateClinicServiceCommandHandler(
        IRepositoryBase<Service, Guid> serviceRepository,
        IRepositoryBase<Category, Guid> categoryRepository,
        IMediaService mediaService,
        IRepositoryBase<ServiceMedia, Guid> _serviceMediaRepository,
        IRepositoryBase<Clinic, Guid> clinicRepository,
        IRepositoryBase<ClinicService, Guid> clinicServiceRepository,
        IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository,
        IRepositoryBase<UserClinic, Guid> userClinicRepository)
    : ICommandHandler<
        CONTRACT.Services.ClinicSerivices.Commands.UpdateClinicServiceCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.ClinicSerivices.Commands.UpdateClinicServiceCommand request,
        CancellationToken cancellationToken)
    {
        // Retrieve Service
        var query = serviceRepository.FindAll(x => x.Id.Equals(request.Id)).AsTracking();
        query = query.Include(x => x.ServiceMedias).Include(x => x.ClinicServices);
        var service = await query.FirstOrDefaultAsync(cancellationToken);

        // Check Service Existence
        if (service == null || service.IsDeleted) return Result.Failure(new Error("404", "Service not found "));

        // Retrieve Category
        var category = await categoryRepository.FindAll(x => x.Id.Equals(request.CategoryId))
            .FirstOrDefaultAsync(cancellationToken);

        // Check Category Existence
        if (category == null || category.IsDeleted) return Result.Failure(new Error("404", "Category not found "));
        
        // Retrieve Clinics
        var clinicIds = request.ClinicId.ToHashSet();
        var clinics = await clinicRepository.FindAll(x => request.ClinicId.Contains(x.Id))
            .ToListAsync(cancellationToken);

        // Check Clinics Existence
        if (clinics.Count != clinicIds.Count) return Result.Failure(new Error("404", "Clinic not found "));

        // Check User Clinic Admin Role
        var userClinicIds = (await userClinicRepository.FindAll(
                    x =>
                        x.UserId.Equals(request.UserId) &&
                        x.User != null &&
                        x.User.Role != null &&
                        x.User.Role.Name.Equals("Clinic Admin"))
                .Select(x => x.ClinicId)
                .ToListAsync(cancellationToken))
            .ToHashSet();

        if(clinics.Where(x => x.ParentId != null).Select(x => x.ParentId).ToHashSet().Any(x => !userClinicIds.Contains(x.Value)))
            return Result.Failure(new Error("403", "You are not authorized to update this service"));
        
        // Update Service Properties
        service.Name = request.Name;
        service.Description = request.Description;
        service.CategoryId = request.CategoryId;

        // Update Clinic Services
        var existingClinicIds = service.ClinicServices.Select(x => x.ClinicId).ToHashSet();
        var clinicsToRemove = existingClinicIds.Except(clinicIds).ToList();

        if (clinicsToRemove.Any())
            clinicServiceRepository.RemoveMultiple(service.ClinicServices
                .Where(x => clinicsToRemove.Contains(x.ClinicId)).ToList());
        
        var newClinicIds = clinicIds.Except(existingClinicIds).ToList();
        if (newClinicIds.Any())
        {
            var newClinicServices = newClinicIds.Select(id => new ClinicService
            {
                Id = Guid.NewGuid(),
                ClinicId = id,
                ServiceId = service.Id
            }).ToList();
            clinicServiceRepository.AddRange(newClinicServices);
        }
        
        List<ServiceMedia> serviceMediaList = new List<ServiceMedia>();
        List<ServiceMedia> serviceMediaListUpdate = new List<ServiceMedia>();
        List<ServiceMedia> serviceMediaListAdd = new List<ServiceMedia>();
        
        if (request.IndexCoverImagesChange?.Count > 0)
        {
            var coverMediaToDelete = service.ServiceMedias?.Where(
                x => x.ServiceMediaType == 0 &&
                     request.IndexCoverImagesChange.Contains(x.IndexNumber) &&
                     !x.IsDeleted).ToList();
                
            var coverMediaToUpdate = service.ServiceMedias?.Where(
                x => x.ServiceMediaType == 0 &&
                     !request.IndexCoverImagesChange.Contains(x.IndexNumber) &&
                     !x.IsDeleted).ToList();

            if (coverMediaToUpdate != null && coverMediaToUpdate.Any())
            {
                var updateCoverImages = coverMediaToUpdate.OrderBy(x => x.IndexNumber).ToList();
                for (int i = 0; i < updateCoverImages.Count(); i++)
                {
                    updateCoverImages[i].IndexNumber = i;
                }
                
                serviceMediaList.AddRange(updateCoverImages);
                serviceMediaListUpdate.AddRange(updateCoverImages);
            }

            if (coverMediaToDelete != null && coverMediaToDelete.Any())
            {
                foreach (var item in coverMediaToDelete)
                {
                    item.IsDeleted = true;
                }
                
                serviceMediaListUpdate.AddRange(coverMediaToDelete);
            }
        }
        
        if (request.CoverImages != null)
        {
            var coverImageFiles = request.CoverImages.Where(x => x.Name == "coverImages").ToList();
            
            var coverImageUrls = await Task.WhenAll(coverImageFiles.Select(mediaService.UploadImageAsync));

            if (coverImageUrls.Any())
            {
                List<ServiceMedia> newCoverMedias;
                if (request.IndexCoverImagesChange?.Count > 0)
                {
                    newCoverMedias = coverImageUrls.Select((x, idx) => new ServiceMedia
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = x,
                        IndexNumber = idx + serviceMediaList.Count,
                        ServiceMediaType = 0,
                        ServiceId = service.Id
                    }).ToList();
                }
                else
                {
                    var oldImgages = service.ServiceMedias?.Where(
                        x => x.ServiceMediaType == 0 && !x.IsDeleted).ToList();
                    
                    newCoverMedias = coverImageUrls.Select((x, idx) => new ServiceMedia
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = x,
                        IndexNumber = idx + oldImgages?.Count ?? 0,
                        ServiceMediaType = 0,
                        ServiceId = service.Id
                    }).ToList();
                }
                serviceMediaList.AddRange(newCoverMedias);
                serviceMediaListAdd.AddRange(newCoverMedias);
            }
        }
        
        // Update Service Media
        if (serviceMediaListUpdate.Any()) _serviceMediaRepository.UpdateRange(serviceMediaListUpdate);
        if (serviceMediaListAdd.Any()) _serviceMediaRepository.AddRange(serviceMediaListAdd);

        // Update Service
        serviceRepository.Update(service);
        
        // Raise Event
        var trigger = TriggerOutbox.RaiseUpdateClinicServiceEvent(
            service.Id, service.Name, service.Description,
            serviceMediaList.Where(x => x.ServiceMediaType == 0).ToArray(),
            [],
            request.CategoryId, category.Name, category.Description ?? "", clinics
        );

        triggerOutboxRepository.Add(trigger);
        
        return Result.Success("Updated Clinic Services Successfully");
    }
}
