using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.ClinicServices;

public class UpdateClinicServiceCommandHandler : ICommandHandler<CONTRACT.Services.ClinicSerivices.Commands.UpdateClinicServiceCommand>
{
    private readonly IRepositoryBase<Service, Guid> _serviceRepository;
    private readonly IRepositoryBase<UserClinic, Guid> _userClinicRepository;
    private readonly IRepositoryBase<Category, Guid> _categoryRepository;
    private readonly IMediaService _mediaService;
    private readonly IRepositoryBase<ServiceMedia, Guid> _serviceMediaRepository;
    private readonly IRepositoryBase<Clinic, Guid> _clinicRepository;
    private readonly IRepositoryBase<ClinicService, Guid> _clinicServiceRepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxRepository;

    public UpdateClinicServiceCommandHandler(IRepositoryBase<Service, Guid> serviceRepository, IRepositoryBase<Category, Guid> categoryRepository, IMediaService mediaService, IRepositoryBase<ServiceMedia, Guid> serviceMediaRepository, IRepositoryBase<Clinic, Guid> clinicRepository, IRepositoryBase<ClinicService, Guid> clinicServiceRepository, IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository, IRepositoryBase<UserClinic, Guid> userClinicRepository)
    {
        _serviceRepository = serviceRepository;
        _categoryRepository = categoryRepository;
        _mediaService = mediaService;
        _serviceMediaRepository = serviceMediaRepository;
        _clinicRepository = clinicRepository;
        _clinicServiceRepository = clinicServiceRepository;
        _triggerOutboxRepository = triggerOutboxRepository;
        _userClinicRepository = userClinicRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.ClinicSerivices.Commands.UpdateClinicServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.FindByIdAsync(request.Id, cancellationToken,
            x => x.ServiceMedias, y=> y.ClinicServices);

        if (service == null || service.IsDeleted)
        {
            return Result.Failure(new Error("404", "Service not found "));
        }
        
        var category = await _categoryRepository.FindAll(x => x.Id.Equals(request.CategoryId)).FirstOrDefaultAsync(cancellationToken);
        
        if (category == null || category.IsDeleted)
        {
            return Result.Failure(new Error("404", "Category not found "));
        }
        
        // Fetch all required clinic data in one go
        var clinicIds = request.ClinicId.ToHashSet();
        var clinics = await _clinicRepository.FindAll(x => request.ClinicId.Contains(x.Id)).ToListAsync(cancellationToken);
        
        if (clinics.Count != clinicIds.Count)
        {
            return Result.Failure(new Error("404", "Clinic not found "));
        }
        
        var userClinicIds = (await _userClinicRepository.FindAll(
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
        
        
        service.Name = request.Name;
        service.Description = request.Description;
        service.CategoryId = request.CategoryId;
            
        var existingClinicIds = service.ClinicServices.Select(x => x.ClinicId).ToHashSet();
        var clinicsToRemove = existingClinicIds.Except(clinicIds).ToList();
        
        if (clinicsToRemove.Any())
        {
            _clinicServiceRepository.RemoveMultiple(service.ClinicServices.Where(x => clinicsToRemove.Contains(x.ClinicId)).ToList());
        }
        
        // Add new clinics
        var newClinicIds = clinicIds.Except(existingClinicIds).ToList();
        if (newClinicIds.Any())
        {
            var newClinicServices = newClinicIds.Select(id => new ClinicService
            {
                Id = Guid.NewGuid(),
                ClinicId = id,
                ServiceId = service.Id
            }).ToList();
            _clinicServiceRepository.AddRange(newClinicServices);
        }
        
        // Handle media updates efficiently
        List<ServiceMedia> serviceMediaList = new List<ServiceMedia>();

        // Process Cover Images
        if (request.IndexCoverImagesChange?.Count > 0 && request.CoverImages != null)
        {
            var coverImageFiles = request.CoverImages.Where(x => x.Name == "coverImages").ToList();
            var coverMediaToDelete = service.ServiceMedias?.Where(
                x => x.ServiceMediaType == 0 &&
                request.IndexCoverImagesChange.Contains(x.IndexNumber) &&
                !x.IsDeleted).ToList();

            if (coverMediaToDelete != null && coverMediaToDelete.Any())
            {
                _serviceMediaRepository.RemoveMultiple(coverMediaToDelete);
            }

            var coverImageUrls = await Task.WhenAll(coverImageFiles.Select(x => _mediaService.UploadImageAsync(x)));
            var newCoverMedias = coverImageUrls.Select((x, idx) => new ServiceMedia
            {
                Id = Guid.NewGuid(),
                ImageUrl = x,
                IndexNumber = request.IndexCoverImagesChange[idx],
                ServiceMediaType = 0,
                ServiceId = service.Id
            }).ToList();

            serviceMediaList.AddRange(newCoverMedias);
        }

        // Process Description Images
        if (request.IndexDescriptionImagesChange?.Count > 0 && request.DescriptionImages != null)
        {
            var descriptionImageFiles = request.DescriptionImages.Where(x => x.Name == "descriptionImages").ToList();
            var descriptionMediaToDelete = service.ServiceMedias?.Where(
                x => x.ServiceMediaType == 1 &&
                request.IndexDescriptionImagesChange.Contains(x.IndexNumber) &&
                !x.IsDeleted).ToList();

            if (descriptionMediaToDelete != null && descriptionMediaToDelete.Any())
            {
                _serviceMediaRepository.RemoveMultiple(descriptionMediaToDelete);
            }

            var descriptionImageUrls = await Task.WhenAll(descriptionImageFiles.Select(x => _mediaService.UploadImageAsync(x)));
            var newDescriptionMedias = descriptionImageUrls.Select((x, idx) => new ServiceMedia
            {
                Id = Guid.NewGuid(),
                ImageUrl = x,
                IndexNumber = request.IndexDescriptionImagesChange[idx],
                ServiceMediaType = 1,
                ServiceId = service.Id
            }).ToList();

            serviceMediaList.AddRange(newDescriptionMedias);
        }

        if (serviceMediaList.Any())
        {
            _serviceMediaRepository.AddRange(serviceMediaList);
        }
        
        _serviceRepository.Update(service);
        
        // Trigger event
        var trigger = TriggerOutbox.RaiseUpdateClinicServiceEvent(
            service.Id, service.Name, service.Description,
            serviceMediaList.Where(x => x.ServiceMediaType == 0).ToArray(),
            serviceMediaList.Where(x => x.ServiceMediaType == 1).ToArray(),
            request.CategoryId, category.Name, category.Description ?? "", clinics
        );
        
        _triggerOutboxRepository.Add(trigger);
        
        return Result.Success("Updated Clinic Services Successfully");
    }
}