using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class StaffUpdateCustomerScheduleTimeCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    IMailService mailService) : ICommandHandler<
    Command.StaffUpdateCustomerScheduleTimeCommand>
{
    public async Task<Result> Handle(Command.StaffUpdateCustomerScheduleTimeCommand request,
        CancellationToken cancellationToken)
    {
        var customerSchedule =
            await customerScheduleRepositoryBase.FindSingleAsync(x => x.Id == request.CustomerScheduleId,
                cancellationToken);
        if (customerSchedule is null)
            return Result.Failure(new Error("404", "Customer Schedule Not Found !"));

        /*  if (customerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
              return Result.Failure(new Error("400", "Cannot update customer schedule that has been completed !"));*/

        var nextCustomerSchedule = await customerScheduleRepositoryBase.FindSingleAsync(
            x => x.OrderId == customerSchedule.OrderId && x.Status == Constant.OrderStatus.ORDER_PENDING &&
                 x.ProcedurePriceType.Procedure.StepIndex ==
                 customerSchedule.ProcedurePriceType.Procedure.StepIndex + 1, cancellationToken);

        if (nextCustomerSchedule == null)
        {
            return Result.Failure(new Error("404", "Next Customer Schedule Not Found !"));
        }

        if (nextCustomerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Next Customer Schedule Already Completed !"));

        nextCustomerSchedule.Date = request.Date;
        nextCustomerSchedule.StartTime = request.StartTime;
        var endTime =
            request.StartTime.Add(TimeSpan.FromHours(nextCustomerSchedule.ProcedurePriceType.Duration / 60.0 + 0.5));

        if (endTime.Hours > 20 || endTime is { Hours: 20, Minutes: > 30 })
            return Result.Failure(new Error("400", "Choose start time soner because clinic close at 20:30"));
        nextCustomerSchedule.EndTime = endTime;
        nextCustomerSchedule.Status = Constant.OrderStatus.ORDER_PENDING;

        customerScheduleRepositoryBase.Update(nextCustomerSchedule);
        nextCustomerSchedule.CustomerScheduleUpdateDateAndTime(nextCustomerSchedule);

        mailService.SendMail(new MailContent
        {
            To = nextCustomerSchedule.Customer.Email,
            Subject = "Yêu cầu lịch hẹn của quý khách vừa được cập nhập",
            Body =
                $"Lịch của quý khách hàng {nextCustomerSchedule.Customer.FullName} đã được cập nhật ngày {nextCustomerSchedule.Date} vào lúc {nextCustomerSchedule.StartTime.ToString()} và sẽ được thẫm mỹ viện duyệt trong 24h "
        });
        return Result.Success();
    }
}