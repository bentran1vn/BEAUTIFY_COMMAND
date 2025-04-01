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

        if (!clinicIds.IsSubsetOf(userClinicIds))
            return Result.Failure(new Error("403", "User is not an admin of one or more clinics"));
        
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
        
        // Handle Cover Images
        if (request.IndexCoverImagesChange?.Count > 0)
        {
            if (request.CoverImages != null)
            {
                var coverImageFiles = request.CoverImages.Where(x => x.Name == "coverImages").ToList();
                var coverMediaToDelete = service.ServiceMedias?.Where(
                    x => x.ServiceMediaType == 0 &&
                         request.IndexCoverImagesChange.Contains(x.IndexNumber) &&
                         !x.IsDeleted).ToList();

                if (coverMediaToDelete != null && coverMediaToDelete.Any())
                    _serviceMediaRepository.RemoveMultiple(coverMediaToDelete);

                var coverImageUrls = await Task.WhenAll(coverImageFiles.Select(x => mediaService.UploadImageAsync(x)));
                var newCoverMedias = coverImageUrls.Select((x, idx) => new ServiceMedia
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = x,
                    IndexNumber = request.IndexCoverImagesChange[idx],
                    ServiceMediaType = 0,
                    ServiceId = service.Id
                }).ToList();

                serviceMediaList.AddRange(newCoverMedias);
                serviceMediaListUpdate.AddRange(newCoverMedias);
            }
            else
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
                    
                    serviceMediaListUpdate.AddRange(updateCoverImages);
                    serviceMediaList.AddRange(updateCoverImages);
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
        }
        
        // Handle Description Images
        if (request.IndexDescriptionImagesChange?.Count > 0)
        {
            if (request.DescriptionImages != null)
            {
                var descriptionImageFiles = request.DescriptionImages.Where(x => x.Name == "descriptionImages").ToList();
                var descriptionMediaToDelete = service.ServiceMedias?.Where(
                    x => x.ServiceMediaType == 1 &&
                         request.IndexDescriptionImagesChange.Contains(x.IndexNumber) &&
                         !x.IsDeleted).ToList();

                if (descriptionMediaToDelete != null && descriptionMediaToDelete.Any())
                    _serviceMediaRepository.RemoveMultiple(descriptionMediaToDelete);

                var descriptionImageUrls =
                    await Task.WhenAll(descriptionImageFiles.Select(x => mediaService.UploadImageAsync(x)));
                var newDescriptionMedias = descriptionImageUrls.Select((x, idx) => new ServiceMedia
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = x,
                    IndexNumber = request.IndexDescriptionImagesChange[idx],
                    ServiceMediaType = 1,
                    ServiceId = service.Id
                }).ToList();

                serviceMediaList.AddRange(newDescriptionMedias);
                serviceMediaListUpdate.AddRange(newDescriptionMedias);
            }
            else
            {
                var coverDescriptionToDelete = service.ServiceMedias?.Where(
                    x => x.ServiceMediaType == 1 &&
                         request.IndexDescriptionImagesChange.Contains(x.IndexNumber) &&
                         !x.IsDeleted).ToList();
                
                var coverDescriptionToUpdate = service.ServiceMedias?.Where(
                    x => x.ServiceMediaType == 1 &&
                         !request.IndexDescriptionImagesChange.Contains(x.IndexNumber) &&
                         !x.IsDeleted).ToList();
                
                if (coverDescriptionToUpdate != null && coverDescriptionToUpdate.Any())
                {
                    var updateCoverImages = coverDescriptionToUpdate.OrderBy(x => x.IndexNumber).ToList();
                    for (int i = 0; i < updateCoverImages.Count(); i++)
                    {
                        updateCoverImages[i].IndexNumber = i;
                    }
                    
                    serviceMediaListUpdate.AddRange(updateCoverImages);
                    serviceMediaList.AddRange(updateCoverImages);
                }

                if (coverDescriptionToDelete != null && coverDescriptionToDelete.Any())
                {

                    foreach (var item in coverDescriptionToDelete)
                    {
                        item.IsDeleted = true;
                    }
                    serviceMediaListUpdate.AddRange(coverDescriptionToDelete);
                }
            }

        }

        // Update Service Media
        if (serviceMediaListUpdate.Any()) _serviceMediaRepository.AddRange(serviceMediaListUpdate);

        // Update Service
        serviceRepository.Update(service);
        
        // Raise Event
        var trigger = TriggerOutbox.RaiseUpdateClinicServiceEvent(
            service.Id, service.Name, service.Description,
            serviceMediaList.Where(x => x.ServiceMediaType == 0).ToArray(),
            serviceMediaList.Where(x => x.ServiceMediaType == 1).ToArray(),
            request.CategoryId, category.Name, category.Description ?? "", clinics
        );

        triggerOutboxRepository.Add(trigger);

        // Return Success
        return Result.Success("Updated Clinic Services Successfully");
    }
}
