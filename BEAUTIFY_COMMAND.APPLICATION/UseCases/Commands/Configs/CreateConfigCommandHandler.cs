using BEAUTIFY_COMMAND.CONTRACT.Services.Configs;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Configs;
internal sealed class CreateConfigCommandHandler(IRepositoryBase<Config, Guid> _config)
    : ICommandHandler<Command.CreateConfigCommand>
{
    public async Task<Result> Handle(Command.CreateConfigCommand request, CancellationToken cancellationToken)
    {
        var isKeyExist = await _config.FindAll(x => x.Key == request.Key).AnyAsync(cancellationToken);
        if (isKeyExist)
        {
            return Result.Failure(new Error("400", "Key already exists"));
        }

        var config = new Config
        {
            Key = request.Key,
            Value = request.Value
        };
        _config.Add(config);
        return Result.Success();
    }
}