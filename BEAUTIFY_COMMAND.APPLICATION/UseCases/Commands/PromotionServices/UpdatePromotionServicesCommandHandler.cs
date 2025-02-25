namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.PromotionServices;

public class UpdatePromotionServicesCommandHandler : ICommandHandler<CONTRACT.Services.ServicePromotions.Commands.UpdatePromotionServicesCommand>
{
    public Task<Result> Handle(CONTRACT.Services.ServicePromotions.Commands.UpdatePromotionServicesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}