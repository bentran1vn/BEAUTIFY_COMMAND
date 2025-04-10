namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.ClinicServices;
public class
    CreateClinicServiceCommandHandler(
        IRepositoryBase<Service, Guid> serviceRepository,
        IRepositoryBase<Category, Guid> categoryRepository,
        IMediaService mediaService,
        IRepositoryBase<ServiceMedia, Guid> serviceMediaRepository,
        IRepositoryBase<Clinic, Guid> clinicRepository,
        IRepositoryBase<ClinicService, Guid> clinicServiceRepository,
        IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository)
    : ICommandHandler<
        CONTRACT.Services.ClinicSerivices.Commands.CreateClinicServiceCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.ClinicSerivices.Commands.CreateClinicServiceCommand request,
        CancellationToken cancellationToken)
    {
        var isCategoryExisted = await categoryRepository.FindByIdAsync(request.CategoryId, cancellationToken);

        if (isCategoryExisted == null || isCategoryExisted.IsDeleted)
            return Result.Failure(new Error("404", "Category not found "));
        
        var isClinicExisted = await clinicRepository.FindAll(x => request.ClinicId.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (isClinicExisted.Count != request.ClinicId.Count)
            return Result.Failure(new Error("404", "Clinic not found "));
        
        var parentClinic = await clinicRepository
            .FindSingleAsync(x => x.Id == request.ParentId
                , cancellationToken);

        if (parentClinic == null || parentClinic.IsDeleted || parentClinic.ParentId != null || parentClinic.IsParent == false)
            return Result.Failure(new Error("404", "Clinic Parent not found "));

        if (!parentClinic.IsActivated)
        {
            return Result.Failure(new Error("404", "Clinic Parent is not activated"));
        }
        
        List<ServiceMedia> serviceMediaList = new List<ServiceMedia>();

        var coverImagesFilter = request.CoverImages.Where(x => x.Name.Equals("coverImages")).ToList();

        var servicesCoverImageTasks = coverImagesFilter.Select(mediaService.UploadImageAsync);

        var coverImageUrls = await Task.WhenAll(servicesCoverImageTasks);

        var service = new Service
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CategoryId = request.CategoryId
        };

        serviceRepository.Add(service);

        var clinicServices = request.ClinicId.Select(x => new ClinicService
        {
            Id = Guid.NewGuid(),
            ServiceId = service.Id,
            ClinicId = x
        });

        clinicServiceRepository.AddRange(clinicServices);

        var medias = coverImageUrls.Select((x, idx) => new ServiceMedia
        {
            Id = Guid.NewGuid(),
            ImageUrl = x,
            IndexNumber = idx,
            ServiceMediaType = 0,
            ServiceId = service.Id
        }).ToList();

        serviceMediaList.AddRange(medias);

        serviceMediaRepository.AddRange(serviceMediaList);

        var trigger = TriggerOutbox.RaiseCreateClinicServiceEvent(
            service.Id, service.Name, service.Description, parentClinic, medias.ToArray(), 
            request.CategoryId, isCategoryExisted.Name, isCategoryExisted.Description ?? "", isClinicExisted
        );

        triggerOutboxRepository.Add(trigger);

        return Result.Success("Clinic service created successfully");
    }
}