using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

/// <summary>
/// customer-schedule/customer
/// </summary>
namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class CustomerRequestScheduleCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    IRepositoryBase<Clinic, Guid> clinicRepositoryBase,
    
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
        if (customerSchedule.Status != Constant.OrderStatus.ORDER_PENDING &&
            customerSchedule.Status != Constant.OrderStatus.ORDER_WAITING_APPROVAL)
            return Result.Failure(new Error("400", "Customer schedule cannot be updated"));
        customerSchedule.Date = request.Date;
        customerSchedule.StartTime = request.StartTime;
        //todo don't hardcode
        var endTime =
            request.StartTime.Add(TimeSpan.FromHours(customerSchedule.ProcedurePriceType.Duration / 60.0));
        var clinic = await clinicRepositoryBase.FindByIdAsync(customerSchedule.Doctor.ClinicId, cancellationToken);

        if (endTime > clinic.WorkingTimeEnd)
            return Result.Failure(new Error("400",
                $"Choose start time soner because clinic close at {clinic.WorkingTimeEnd}"));
        customerSchedule.EndTime = endTime;
        customerSchedule.Status = Constant.OrderStatus.ORDER_WAITING_APPROVAL;

        customerScheduleRepositoryBase.Update(customerSchedule);
        customerSchedule.CustomerScheduleUpdateDateAndTime(customerSchedule);

        _ = mailService.SendMail(new MailContent
        {
            To = customerSchedule.Customer.Email,
            Subject = "Yêu cầu lịch hẹn đã được cập nhật",
            Body =
                $"Lịch của bạn đã được cập nhật {customerSchedule.StartTime.ToString()} và sẽ được thẫm mỹ viện duyệt trong 24h "
        });
        return Result.Success();
    }
}