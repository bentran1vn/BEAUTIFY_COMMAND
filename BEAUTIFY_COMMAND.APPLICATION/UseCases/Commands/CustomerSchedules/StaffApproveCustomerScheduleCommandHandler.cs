using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class StaffApproveCustomerScheduleCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    IMailService mailService)
    : ICommandHandler<Command.StaffApproveCustomerScheduleCommand>
{
    public async Task<Result> Handle(Command.StaffApproveCustomerScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var customerSchedule = await customerScheduleRepositoryBase.FindSingleAsync(
            x => x.Id == request.CustomerScheduleId && x.Status == Constant.OrderStatus.ORDER_PENDING,
            cancellationToken);

        if (customerSchedule == null)
            return Result.Failure(new Error("404", "Customer schedule not found"));
        if (customerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Customer schedule already completed"));

        switch (request.Status)
        {
            case "Approved":
                customerSchedule.Status = Constant.OrderStatus.ORDER_PENDING;
                customerScheduleRepositoryBase.Update(customerSchedule);
                customerSchedule.UpdateCustomerScheduleStatus(request.CustomerScheduleId, request.Status);

                mailService.SendMail(new MailContent
                {
                    To = customerSchedule.Customer.Email,
                    Subject = "Lịch hẹn của bạn đã được duyệt",
                    Body =
                        $"Lịch hẹn của bạn vào {customerSchedule.Date} từ {customerSchedule.StartTime} đến {customerSchedule.EndTime} đã được duyệt."
                });
                break;
            case "Rejected":
                mailService.SendMail(new MailContent
                {
                    To = customerSchedule.Customer.Email,
                    Subject = "Lịch hẹn của bạn đã bị từ chối",
                    Body =
                        $"Lịch hẹn của bạn vào {customerSchedule.Date} từ {customerSchedule.StartTime} đến {customerSchedule.EndTime} đã bị từ chối. Vui lòng liên hệ thẩm mỹ viện để biết thêm chi tiết."
                });
                break;
        }

        return Result.Success();
    }
}