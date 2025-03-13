namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.PromotionServices;
public class
    DeletePromotionServicesCommandHandler : ICommandHandler<
    CONTRACT.Services.ServicePromotions.Commands.DeletePromotionServicesCommand>
{
    public Task<Result> Handle(CONTRACT.Services.ServicePromotions.Commands.DeletePromotionServicesCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}