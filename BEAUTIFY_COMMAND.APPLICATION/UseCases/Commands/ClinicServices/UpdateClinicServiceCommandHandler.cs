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
        var userClinicIds = (await userClinicRepository.FindAll(x =>
                    x.UserId.Equals(request.UserId) &&
                    x.User != null &&
                    x.User.Role != null &&
                    x.User.Role.Name.Equals("Clinic Admin"))
                .Select(x => x.ClinicId)
                .ToListAsync(cancellationToken))
            .ToHashSet();

        if (clinics.Where(x => x.ParentId != null).Select(x => x.ParentId).ToHashSet()
            .Any(x => !userClinicIds.Contains(x.Value)))
            return Result.Failure(new Error("403", "You are not authorized to update this service"));

        // Update Service Properties
        service.Name = request.Name;
        service.Description = request.Description;
        service.CategoryId = request.CategoryId;
        service.IsRefundable = request.IsRefundable;
        service.DepositPercent = request.DepositPercent;

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

        // Get all existing, non-deleted cover images
        var existingCoverImages = service.ServiceMedias?
            .Where(x => x.ServiceMediaType == 0 && !x.IsDeleted)
            .OrderBy(x => x.IndexNumber)
            .ToList() ?? new List<ServiceMedia>();

        var updatedCoverImages = new List<ServiceMedia>();
        var servicesToUpdate = new List<ServiceMedia>();

        // Handle image removals if specified
        if (request.IndexCoverImagesChange != null && request.IndexCoverImagesChange.Any())
        {
            // Mark images for deletion
            foreach (var indexToRemove in request.IndexCoverImagesChange)
            {
                var imageToDelete = existingCoverImages.FirstOrDefault(x => x.IndexNumber == indexToRemove);
                if (imageToDelete != null)
                {
                    imageToDelete.IsDeleted = true;
                    servicesToUpdate.Add(imageToDelete);
                }
            }

            // Get remaining images after deletion
            var remainingImages = existingCoverImages
                .Where(x => !request.IndexCoverImagesChange.Contains(x.IndexNumber))
                .ToList();

            // Reindex remaining images starting from 0
            for (var i = 0; i < remainingImages.Count; i++)
            {
                var image = remainingImages[i];
                image.IndexNumber = i;
                servicesToUpdate.Add(image);
                updatedCoverImages.Add(image);
            }
        }
        else
        {
            // If no deletions, keep all existing images with their current indices
            updatedCoverImages.AddRange(existingCoverImages);
        }

        // Handle new image uploads
        var newCoverImages = new List<ServiceMedia>();
        if (request.CoverImages != null && request.CoverImages.Any())
        {
            var coverImageFiles = request.CoverImages.Where(x => x.Name == "coverImages").ToList();
            
            if (coverImageFiles.Any())
            {
                var coverImageUrls = await Task.WhenAll(coverImageFiles.Select(mediaService.UploadImageAsync));
                
                // Create new image entities with indices continuing from existing ones
                int nextIndex = updatedCoverImages.Count > 0 
                    ? updatedCoverImages.Max(x => x.IndexNumber) + 1 
                    : 0;
                    
                foreach (var imageUrl in coverImageUrls)
                {
                    var newMedia = new ServiceMedia
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = imageUrl,
                        IndexNumber = nextIndex++,
                        ServiceMediaType = 0,
                        ServiceId = service.Id
                    };
                    
                    newCoverImages.Add(newMedia);
                    updatedCoverImages.Add(newMedia);
                }
            }
        }

        // Update database
        if (servicesToUpdate.Any())
        {
            _serviceMediaRepository.UpdateRange(servicesToUpdate);
        }

        if (newCoverImages.Any())
        {
            _serviceMediaRepository.AddRange(newCoverImages);
        }

        // Combine all updated cover images for the event trigger
        var finalCoverImages = updatedCoverImages.ToArray();

        // Update Service
        serviceRepository.Update(service);

        // Raise Event
        var trigger = TriggerOutbox.RaiseUpdateClinicServiceEvent(
            service.Id, service.Name, service.Description,
            service.DepositPercent, service.IsRefundable,
            finalCoverImages,
            [],
            request.CategoryId, category.Name, category.Description ?? "", clinics
        );

        triggerOutboxRepository.Add(trigger);

        return Result.Success("Updated Clinic Services Successfully");
    }
}