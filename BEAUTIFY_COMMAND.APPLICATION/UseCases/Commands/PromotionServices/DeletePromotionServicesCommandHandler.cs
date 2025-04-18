namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.PromotionServices;
public class
    DeletePromotionServicesCommandHandler : ICommandHandler<
    CONTRACT.Services.ServicePromotions.Commands.DeletePromotionServicesCommand>
{
    private readonly IRepositoryBase<ClinicService, Guid> _clinicServicerepository;
    private readonly IMediaService _mediaService;
    private readonly IRepositoryBase<Promotion, Guid> _promotionrepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxrepository;

    public DeletePromotionServicesCommandHandler(IRepositoryBase<ClinicService, Guid> clinicServicerepository,
        IMediaService mediaService, IRepositoryBase<Promotion, Guid> promotionrepository,
        IRepositoryBase<TriggerOutbox, Guid> triggerOutboxrepository)
    {
        _clinicServicerepository = clinicServicerepository;
        _mediaService = mediaService;
        _promotionrepository = promotionrepository;
        _triggerOutboxrepository = triggerOutboxrepository;
    }

    public async Task<Result> Handle(
        CONTRACT.Services.ServicePromotions.Commands.DeletePromotionServicesCommand request,
        CancellationToken cancellationToken)
    {
        var query = _promotionrepository.FindAll(x => x.Id.Equals(request.PromotionId)).AsTracking();

        query = query.Include(x => x.Service);

        var promotionExist = await query.FirstOrDefaultAsync(cancellationToken);

        if (promotionExist == null || promotionExist.IsDeleted)
            return Result.Failure(new Error("404", "Promotion not found !"));

        var isValidServiceQuery = _clinicServicerepository.FindAll(x =>
            x.ServiceId == promotionExist.ServiceId &&
            (x.ClinicId == request.ClinicId || x.Clinics.ParentId == request.ClinicId) &&
            !x.IsDeleted
        );

        var isValidService = await isValidServiceQuery.FirstOrDefaultAsync(cancellationToken);

        if (isValidService == null)
            return Result.Failure(new Error("401", "Can not delete promotion for this service"));

        promotionExist.Service!.DiscountPrice = (decimal)promotionExist.DiscountPercent;
        _promotionrepository.Remove(promotionExist);

        var trigger = TriggerOutbox.RaiseDeletePromotionEvent(
            promotionExist.Id,
            (Guid)promotionExist.ServiceId!
        );

        _triggerOutboxrepository.Add(trigger);

        return Result.Success("Delete Promotion Successfully !");
    }
}