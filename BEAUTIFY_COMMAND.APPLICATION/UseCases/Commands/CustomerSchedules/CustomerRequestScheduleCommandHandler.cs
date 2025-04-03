using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class CustomerRequestScheduleCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase)
    : ICommandHandler<Command.CustomerRequestScheduleCommand>
{
    public async Task<Result> Handle(Command.CustomerRequestScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var customerSchedule = await customerScheduleRepositoryBase.FindByIdAsync(request.CustomerScheduleId,
            cancellationToken);
        if (customerSchedule == null)
            return Result.Failure(new Error("404", "Customer schedule not found"));
        if (customerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Customer schedule already completed"));
        customerSchedule.Date = request.Date;
        customerSchedule.StartTime = request.StartTime;
        var endTime = customerSchedule.ProcedurePriceType.Duration / 60.0 + 0.5;
        customerSchedule.EndTime = request.StartTime.Add(TimeSpan.FromHours(endTime));
        customerScheduleRepositoryBase.Update(customerSchedule);
        customerSchedule.CustomerScheduleUpdateDateAndTime(customerSchedule);
        return Result.Success();
    }
}