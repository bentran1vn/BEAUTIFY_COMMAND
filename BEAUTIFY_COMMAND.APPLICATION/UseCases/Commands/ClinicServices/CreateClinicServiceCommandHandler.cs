using AutoMapper;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.ClinicServices;

public class CreateClinicServiceCommandHandler : ICommandHandler<CONTRACT.Services.ClinicSerivices.Commands.CreateClinicServiceCommand>
{
    private readonly IRepositoryBase<Service, Guid> _serviceRepository;
    private readonly IRepositoryBase<Category, Guid> _categoryRepository;
    private readonly IMediaService _mediaService;
    private readonly IRepositoryBase<ServiceMedia, Guid> _serviceMediaRepository;
    private readonly IRepositoryBase<Clinic, Guid> _clinicRepository;
    private readonly IRepositoryBase<ClinicService, Guid> _clinicServiceRepository;

    public CreateClinicServiceCommandHandler(IRepositoryBase<Service, Guid> serviceRepository, IRepositoryBase<Category, Guid> categoryRepository, IMediaService mediaService, IRepositoryBase<ServiceMedia, Guid> serviceMediaRepository, IRepositoryBase<Clinic, Guid> clinicRepository, IRepositoryBase<ClinicService, Guid> clinicServiceRepository)
    {
        _serviceRepository = serviceRepository;
        _categoryRepository = categoryRepository;
        _mediaService = mediaService;
        _serviceMediaRepository = serviceMediaRepository;
        _clinicRepository = clinicRepository;
        _clinicServiceRepository = clinicServiceRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.ClinicSerivices.Commands.CreateClinicServiceCommand request, CancellationToken cancellationToken)
    {
        var isCategoryExisted = await _categoryRepository.FindByIdAsync(request.CategoryId, cancellationToken);
        
        if (isCategoryExisted == null || isCategoryExisted.IsDeleted)
        {
            return Result.Failure(new Error("404", "Category not found "));
        }
        
        var isClinicExisted = await _clinicRepository.FindAll(x => request.ClinicId.Contains(x.Id)).ToListAsync(cancellationToken);
        
        if (isClinicExisted.Count != request.ClinicId.Length)
        {
            return Result.Failure(new Error("404", "Clinic not found "));
        }
        
        List<ServiceMedia> serviceMediaList = new List<ServiceMedia>();

        var coverImagesFilter = request.CoverImages.Where(x => x.Name.Equals("coverImages")).ToList();
        
        var servicesCoverImageTasks = coverImagesFilter.Select(x => _mediaService.UploadImageAsync(x));
        
        var coverImageUrls = await Task.WhenAll(servicesCoverImageTasks);
        
        var descriptionImagesFilter = request.DescriptionImages.Where(x => x.Name.Equals("descriptionImages")).ToList();
        
        var servicesDescriptionImageTasks = descriptionImagesFilter.Select(x => _mediaService.UploadImageAsync(x));
        
        var desImageUrls = await Task.WhenAll(servicesDescriptionImageTasks);
        
        var service = Service.RaiseCreateClinicServiceEvent(
            request.Name, request.Description,
            coverImageUrls, desImageUrls, request.Price,
            isCategoryExisted.Id, isCategoryExisted.Name, isCategoryExisted.Description ?? "",
            isClinicExisted);
        
        _serviceRepository.Add(service);

        var clinicServices = request.ClinicId.Select(x => new ClinicService()
        {
            Id = Guid.NewGuid(),
            ServiceId = service.Id,
            ClinicId = x
        });
        
        _clinicServiceRepository.AddRange(clinicServices);
        
        var medias = coverImageUrls.Select((x, idx) => new ServiceMedia()
        {
            Id = Guid.NewGuid(),
            ImageUrl = x,
            IndexNumber = idx,
            ServiceMediaType = 0,
            ServiceId = service.Id
        }).ToList();
        
        serviceMediaList.AddRange(medias);
        
        var desMedias = desImageUrls.Select((x, idx) => new ServiceMedia()
        {
            Id = Guid.NewGuid(),
            ImageUrl = x,
            IndexNumber = idx,
            ServiceMediaType = 1,
            ServiceId = service.Id
        }).ToList();
        
        serviceMediaList.AddRange(desMedias);
        
        _serviceMediaRepository.AddRange(serviceMediaList);
        
        return Result.Success("Clinic service created successfully");
    }
}