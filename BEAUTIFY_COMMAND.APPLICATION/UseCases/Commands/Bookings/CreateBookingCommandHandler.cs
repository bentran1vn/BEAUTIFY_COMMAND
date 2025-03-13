namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Bookings;
internal sealed class
    CreateBookingCommandHandler(
        IRepositoryBase<User, Guid> userRepositoryBase,
        IRepositoryBase<Service, Guid> serviceRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Bookings.Commands.CreateBookingCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Bookings.Commands.CreateBookingCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}