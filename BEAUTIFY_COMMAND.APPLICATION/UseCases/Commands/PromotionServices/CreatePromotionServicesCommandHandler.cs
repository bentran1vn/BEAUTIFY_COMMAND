using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.PromotionServices;
public class
    CreatePromotionServicesCommandHandler : ICommandHandler<
    CONTRACT.Services.ServicePromotions.Commands.CreatePromotionServicesCommand>
{
    private readonly IRepositoryBase<ClinicService, Guid> _clinicServicerepository;
    private readonly IMediaService _mediaService;
    private readonly IRepositoryBase<Promotion, Guid> _promotionrepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxrepository;
    private readonly IRepositoryBase<UserClinic, Guid> _userClinicrepository;

    public CreatePromotionServicesCommandHandler(IRepositoryBase<UserClinic, Guid> userClinicrepository,
        IRepositoryBase<ClinicService, Guid> clinicServicerepository, IMediaService mediaService,
        IRepositoryBase<Promotion, Guid> promotionrepository,
        IRepositoryBase<TriggerOutbox, Guid> triggerOutboxrepository)
    {
        _userClinicrepository = userClinicrepository;
        _clinicServicerepository = clinicServicerepository;
        _mediaService = mediaService;
        _promotionrepository = promotionrepository;
        _triggerOutboxrepository = triggerOutboxrepository;
    }

    public async Task<Result> Handle(
        CONTRACT.Services.ServicePromotions.Commands.CreatePromotionServicesCommand request,
        CancellationToken cancellationToken)
    {
        var isAuth = await _userClinicrepository.FindSingleAsync(
            x =>
                x.UserId == request.UserId && x.User != null &&
                x.User.Role != null && x.User.Role.Name.Equals("Clinic Admin") &&
                x.ClinicId == request.ClinicId && !x.IsDeleted
            , cancellationToken);


        if (isAuth == null) return Result.Failure(new Error("401", "Unauthorized Access"));

        var isValidServiceQuery = _clinicServicerepository.FindAll(
            x =>
                x.ServiceId == request.ServiceId &&
                (x.ClinicId == request.ClinicId || x.Clinics.ParentId == request.ClinicId) &&
                !x.IsDeleted
        ).AsTracking();

        isValidServiceQuery = isValidServiceQuery
            .Include(x => x.Clinics)
            .Include(x => x.Services)
            .ThenInclude(x => x.Procedures)!
            .ThenInclude(x => x.ProcedurePriceTypes);

        var isValidService = await isValidServiceQuery.FirstOrDefaultAsync(cancellationToken);

        if (isValidService == null) return Result.Failure(new Error("404", "Service Not Found"));

        var lastestPromotion = await _promotionrepository.FindSingleAsync(
            x => x.IsActivated && !x.IsDeleted, cancellationToken);

        if (lastestPromotion != null) lastestPromotion.IsActivated = false;

        var imageUrl = await _mediaService.UploadImageAsync(request.Image);

        var promotion = new Promotion
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            ServiceId = request.ServiceId,
            DiscountPercent = request.DiscountPercent / 100,
            StartDate = DateTimeOffset.Parse(request.StartDay.ToString()),
            EndDate = DateTimeOffset.Parse(request.EndDate.ToString()),
            ImageUrl = imageUrl,
            IsActivated = true
        };

        _promotionrepository.Add(promotion);

        var lowestPrice = isValidService.Services?.Procedures?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Min(pt => pt.Price)
                : 0) ?? 0;

        var highestPrice = isValidService.Services?.Procedures?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Max(pt => pt.Price)
                : 0) ?? 0;

        isValidService.Services!.MinPrice = lowestPrice * (decimal)promotion.DiscountPercent;
        isValidService.Services!.MaxPrice = highestPrice * (decimal)promotion.DiscountPercent;

        var trigger = TriggerOutbox.RaiseCreatePromotionEvent(
            promotion.Id, request.ServiceId, promotion.Name,
            promotion.DiscountPercent, promotion.ImageUrl,
            highestPrice - highestPrice * (decimal)promotion.DiscountPercent,
            lowestPrice - lowestPrice * (decimal)promotion.DiscountPercent,
            request.StartDay, request.EndDate);

        _triggerOutboxrepository.Add(trigger);

        return Result.Success("Create Promotion Successfully !");
    }
}