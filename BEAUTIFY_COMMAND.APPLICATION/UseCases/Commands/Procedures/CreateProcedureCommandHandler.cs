using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Procedures;
public class
    CreateProcedureCommandHandler : ICommandHandler<CONTRACT.Services.Procedures.Commands.CreateProcedureCommand>
{
    private readonly IRepositoryBase<Service, Guid> _clinicServiceRepository;
    private readonly IMediaService _mediaService;
    private readonly IRepositoryBase<ProcedureMedia, Guid> _procedureMediaServiceRepository;
    private readonly IRepositoryBase<ProcedurePriceType, Guid> _procedurePriceTypeServiceRepository;
    private readonly IRepositoryBase<Procedure, Guid> _procedureServiceRepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxRepository;

    public CreateProcedureCommandHandler(IRepositoryBase<Service, Guid> clinicServiceRepository,
        IMediaService mediaService, IRepositoryBase<Procedure, Guid> procedureServiceRepository,
        IRepositoryBase<ProcedureMedia, Guid> procedureMediaServiceRepository,
        IRepositoryBase<ProcedurePriceType, Guid> procedurePriceTypeServiceRepository,
        IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository)
    {
        _clinicServiceRepository = clinicServiceRepository;
        _mediaService = mediaService;
        _procedureServiceRepository = procedureServiceRepository;
        _procedureMediaServiceRepository = procedureMediaServiceRepository;
        _procedurePriceTypeServiceRepository = procedurePriceTypeServiceRepository;
        _triggerOutboxRepository = triggerOutboxRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Procedures.Commands.CreateProcedureCommand request,
        CancellationToken cancellationToken)
    {
        var isExisted = await _clinicServiceRepository.FindByIdAsync(request.ClinicServiceId, cancellationToken,
            x => x.Promotions);

        if (isExisted == null || isExisted.IsDeleted) return Result.Failure(new Error("404", "Services not found !"));

        if (isExisted.Procedures != null &&
            isExisted.Procedures.Any(p => p.StepIndex == request.StepIndex && p.IsDeleted == false))
            return Result.Failure(new Error("400", "Step Index Exist !"));

        if (request.ProcedurePriceTypes == null || request.ProcedurePriceTypes.Count() == 0)
            return Result.Failure(new Error("400", "No Price Types !"));

        var procedure = new Procedure
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            StepIndex = request.StepIndex,
            ServiceId = request.ClinicServiceId
        };

        _procedureServiceRepository.Add(procedure);

        var coverImageTasks = request.ProcedureCoverImage.Select(x => _mediaService.UploadImageAsync(x));

        var coverImageUrls = await Task.WhenAll(coverImageTasks);

        var medias = coverImageUrls.Select((x, idx) => new ProcedureMedia
        {
            Id = Guid.NewGuid(),
            ImageUrl = x,
            IndexNumber = idx,
            ProcedureId = procedure.Id
        }).ToList();

        _procedureMediaServiceRepository.AddRange(medias);

        var procedurePriceTypes = request.ProcedurePriceTypes.Select(x => new ProcedurePriceType
        {
            Id = Guid.NewGuid(),
            Name = x.Name,
            Price = x.Price,
            ProcedureId = procedure.Id
        });

        var priceTypes = procedurePriceTypes.ToList();

        _procedurePriceTypeServiceRepository.AddRange(priceTypes);

        var lowestPrice = isExisted.Procedures?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Min(pt => pt.Price)
                : 0) ?? 0;

        var highestPrice = isExisted.Procedures?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Max(pt => pt.Price)
                : 0) ?? 0;

        var discountPercent = isExisted.Promotions?.FirstOrDefault(x => x.IsActivated)?.DiscountPercent;

        var trigger = TriggerOutbox.RaiseCreateServiceProcedureEvent(
            procedure.Id, isExisted.Id, procedure.Name, procedure.Description,
            highestPrice, lowestPrice,
            highestPrice - (decimal?)discountPercent * highestPrice,
            lowestPrice - (decimal?)discountPercent * lowestPrice,
            procedure.StepIndex, coverImageUrls, priceTypes);

        _triggerOutboxRepository.Add(trigger);

        return Result.Success("Create Service's Procedure Successfully");
    }
}