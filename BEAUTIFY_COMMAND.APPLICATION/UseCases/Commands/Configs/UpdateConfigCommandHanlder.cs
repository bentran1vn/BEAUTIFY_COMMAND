using BEAUTIFY_COMMAND.CONTRACT.Services.Configs;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Configs;
internal sealed class UpdateConfigCommandHanlder(IRepositoryBase<Config, Guid> repositoryBase)
    : ICommandHandler<Command.UpdateConfigCommand>
{
    public async Task<Result> Handle(Command.UpdateConfigCommand request, CancellationToken cancellationToken)
    {
        var config = await repositoryBase.FindByIdAsync(request.Id, cancellationToken);
        if (config is null)
        {
            return Result.Failure(new Error("404", "Config not found"));
        }
        
        config.Value = request.Value;
        repositoryBase.Update(config);
        return Result.Success();
    }
}