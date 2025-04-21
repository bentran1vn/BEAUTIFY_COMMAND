using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

/// <summary>
/// customer-schedule/customer
/// </summary>
namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class CustomerRequestScheduleCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    IMailService mailService)
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
        var endTime =
            request.StartTime.Add(TimeSpan.FromHours(customerSchedule.ProcedurePriceType.Duration / 60.0 + 0.5));

        if (endTime.Hours > 20 || endTime is { Hours: 20, Minutes: > 30 })
            return Result.Failure(new Error("400", "Choose start time soner because clinic close at 20:30"));
        customerSchedule.EndTime = endTime;
        customerSchedule.Status = Constant.OrderStatus.ORDER_WAITING_APPROVAL;

        customerScheduleRepositoryBase.Update(customerSchedule);
        customerSchedule.CustomerScheduleUpdateDateAndTime(customerSchedule);

        mailService.SendMail(new MailContent
        {
            To = customerSchedule.Customer.Email,
            Subject = "Yêu cầu lịch hẹn đã được cập nhật",
            Body =
                $"Lịch của bạn đã được cập nhật {customerSchedule.StartTime.ToString()} và sẽ được thẫm mỹ viện duyệt trong 24h "
        });
        return Result.Success();
    }
}