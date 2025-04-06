namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.PromotionServices;
public class
    UpdatePromotionServicesCommandHandler : ICommandHandler<
    CONTRACT.Services.ServicePromotions.Commands.UpdatePromotionServicesCommand>
{
    private readonly IRepositoryBase<ClinicService, Guid> _clinicServicerepository;
    private readonly IMediaService _mediaService;
    private readonly IRepositoryBase<Promotion, Guid> _promotionrepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxrepository;

    public UpdatePromotionServicesCommandHandler(IRepositoryBase<ClinicService, Guid> clinicServicerepository, IMediaService mediaService, IRepositoryBase<Promotion, Guid> promotionrepository, IRepositoryBase<TriggerOutbox, Guid> triggerOutboxrepository)
    {
        _clinicServicerepository = clinicServicerepository;
        _mediaService = mediaService;
        _promotionrepository = promotionrepository;
        _triggerOutboxrepository = triggerOutboxrepository;
    }


    public async Task<Result> Handle(CONTRACT.Services.ServicePromotions.Commands.UpdatePromotionServicesCommand request,
        CancellationToken cancellationToken)
    {
        var query = _promotionrepository.FindAll(x => x.Id.Equals(request.PromotionId)).AsTracking();

        query = query.Include(x => x.Service);

        var promotionExist = await query.FirstOrDefaultAsync(cancellationToken);
        
        if(promotionExist == null || promotionExist.IsDeleted)
            return Result.Failure(new Error("404", "Promotion not found !"));
        
        var isValidServiceQuery = _clinicServicerepository.FindAll(
            x =>
                x.ServiceId == promotionExist.ServiceId &&
                (x.ClinicId == request.ClinicId || x.Clinics.ParentId == request.ClinicId) &&
                !x.IsDeleted
        );

        var isValidService = await isValidServiceQuery.FirstOrDefaultAsync(cancellationToken);

        if (isValidService == null) return Result.Failure(new Error("401", "Can not update promotion for this service"));
        
        if(request.StartDay < DateTimeOffset.UtcNow)
            return Result.Failure(new Error("400", "Start day must be greater than current date"));
        
        if(request.EndDate < DateTimeOffset.UtcNow)
            return Result.Failure(new Error("400", "End date must be greater than current date"));
        
        if (request.StartDay > request.EndDate)
            return Result.Failure(new Error("400", "Start day must be less than end date"));
        
        promotionExist.Name = request.Name;
        promotionExist.StartDate = DateTimeOffset.Parse(request.StartDay.ToString());
        promotionExist.EndDate = DateTimeOffset.Parse(request.EndDate.ToString());
        promotionExist.DiscountPercent = request.DiscountPercent / 100;
        
        if (request.Image != null)
        {
            var imageUrl = await _mediaService.UploadImageAsync(request.Image);
            promotionExist.ImageUrl = imageUrl;
        }
        
        if (promotionExist.IsActivated == false && request.IsActivated)
        {
            var queryLast = _promotionrepository.FindAll(
                x => x.IsActivated && !x.IsDeleted && x.LivestreamRoomId == null);
            
            var lastestPromotion = await queryLast.FirstOrDefaultAsync(cancellationToken);

            if (lastestPromotion != null && lastestPromotion.IsDeleted == false)
            {
                lastestPromotion.IsActivated = false;
                _promotionrepository.Update(lastestPromotion);
            }
            
            promotionExist.IsActivated = request.IsActivated;
        }
        
        promotionExist.IsActivated = request.IsActivated;
        
        promotionExist.Service!.DiscountPrice = (decimal)promotionExist.DiscountPercent;
        
        var trigger = TriggerOutbox.RaiseUpdatePromotionEvent(
            promotionExist.Id, (Guid)promotionExist.ServiceId!, request.Name,
            request.DiscountPercent / 100, promotionExist.ImageUrl ?? "",
            promotionExist.Service.MaxPrice - promotionExist.Service.MaxPrice * (decimal)promotionExist.DiscountPercent,
            promotionExist.Service.MinPrice - promotionExist.Service.MinPrice * (decimal)promotionExist.DiscountPercent,
            request.StartDay, request.EndDate, request.IsActivated);

        _triggerOutboxrepository.Add(trigger);
        
        return Result.Success("Update Promotion Successfully !");
    }
}