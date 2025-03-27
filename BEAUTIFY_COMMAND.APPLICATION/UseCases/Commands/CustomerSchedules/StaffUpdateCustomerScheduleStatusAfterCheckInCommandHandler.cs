using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class StaffUpdateCustomerScheduleStatusAfterCheckInCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    ICurrentUserService currentUserService)
    : ICommandHandler<Command.StaffUpdateCustomerScheduleStatusAfterCheckInCommand>
{
    public async Task<Result> Handle(Command.StaffUpdateCustomerScheduleStatusAfterCheckInCommand request,
        CancellationToken cancellationToken)
    {
        //take the vn time zone
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var checkInDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone);
        var customerSchedule = await customerScheduleRepositoryBase.FindByIdAsync(request.CustomerScheduleId,
            cancellationToken);
        if (customerSchedule!.Doctor!.ClinicId != currentUserService.ClinicId)
            return Result.Failure(new Error("403", "User is not a staff of this clinic"));
        if (customerSchedule == null)
            return Result.Failure(new Error("404", "Customer Schedule Not Found"));
        if (customerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Customer Schedule already completed"));
        if (customerSchedule.Date != DateOnly.FromDateTime(checkInDate.Date))
            return Result.Failure(new Error("400", "Customer Schedule date is not today"));
        customerSchedule.Status = request.Status;
        customerScheduleRepositoryBase.Update(customerSchedule);
        customerSchedule.UpdateCustomerScheduleStatus(customerSchedule.Id, request.Status);
        return Result.Success();
    }
}