using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class StaffUpdateCustomerScheduleTimeCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepositoryBase,
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

        if (request.IsNext)
        {
            var nextCustomerSchedule = await customerScheduleRepositoryBase.FindSingleAsync(
                x => x.OrderId == customerSchedule.OrderId && x.Status == Constant.OrderStatus.ORDER_PENDING &&
                     x.ProcedurePriceType.Procedure.StepIndex ==
                     customerSchedule.ProcedurePriceType.Procedure.StepIndex + 1, cancellationToken);

            if (nextCustomerSchedule == null)
                return Result.Failure(new Error("404", "Next Customer Schedule Not Found !"));

            if (nextCustomerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
                return Result.Failure(new Error("400", "Next Customer Schedule Already Completed !"));

            var workingSchedule = await
                workingScheduleRepositoryBase.FindAll(x =>
                    x.Date == request.Date && x.DoctorClinicId == customerSchedule.DoctorId &&
                    x.StartTime == request.StartTime).ToListAsync(cancellationToken);

            if (workingSchedule.Count != 0)
                return Result.Failure(new Error("400", "Doctor is busy at this time !"));

            nextCustomerSchedule.Date = request.Date;
            nextCustomerSchedule.StartTime = request.StartTime;
            var endTime =
                request.StartTime.Add(
                    TimeSpan.FromHours(nextCustomerSchedule.ProcedurePriceType.Duration / 60.0 + 0.5));

            if (endTime.Hours > 20 || endTime is { Hours: 20, Minutes: > 30 })
                return Result.Failure(new Error("400", "Choose start time soner because clinic close at 20:30"));
            nextCustomerSchedule.EndTime = endTime;
            nextCustomerSchedule.Status = Constant.OrderStatus.ORDER_PENDING;

            customerScheduleRepositoryBase.Update(nextCustomerSchedule);
            nextCustomerSchedule.CustomerScheduleUpdateDateAndTime(nextCustomerSchedule);
            var doctorSchedule = new WorkingSchedule
            {
                Id = Guid.NewGuid(),
                CustomerScheduleId = customerSchedule.Id,
                DoctorClinicId = customerSchedule.DoctorId,
                StartTime = request.StartTime,
                EndTime = endTime,
                Date = customerSchedule.Date.Value
            };
            workingScheduleRepositoryBase.Add(doctorSchedule);
            doctorSchedule.WorkingScheduleCreate(customerSchedule.Doctor.UserId, customerSchedule.Doctor.ClinicId,
                customerSchedule.Doctor.User.FirstName + " " + customerSchedule.Doctor.User.LastName,
                [doctorSchedule], customerSchedule);
            mailService.SendMail(new MailContent
            {
                To = nextCustomerSchedule.Customer.Email,
                Subject = "Yêu cầu lịch hẹn của quý khách vừa được cập nhập",
                Body =
                    $"Lịch của quý khách hàng {nextCustomerSchedule.Customer.FullName} đã được cập nhật ngày {nextCustomerSchedule.Date} vào lúc {nextCustomerSchedule.StartTime.ToString()}"
            });
        }
        else
        {
            if (customerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
                return Result.Failure(new Error("400", "Customer Schedule Already Completed !"));
            var workingSchedule = await
                workingScheduleRepositoryBase.FindAll(x =>
                    x.Date == request.Date && x.DoctorClinicId == customerSchedule.DoctorId &&
                    x.StartTime == request.StartTime).ToListAsync(cancellationToken);

            if (workingSchedule.Count != 0)
                return Result.Failure(new Error("400", "Doctor is busy at this time !"));

            customerSchedule.Date = request.Date;
            customerSchedule.StartTime = request.StartTime;
            var endTime =
                request.StartTime.Add(TimeSpan.FromHours(customerSchedule.ProcedurePriceType.Duration / 60.0 + 0.5));

            if (endTime.Hours > 20 || endTime is { Hours: 20, Minutes: > 30 })
                return Result.Failure(new Error("400", "Choose start time soner because clinic close at 20:30"));
            customerSchedule.EndTime = endTime;
            customerSchedule.Status = Constant.OrderStatus.ORDER_PENDING;

            customerScheduleRepositoryBase.Update(customerSchedule);
            customerSchedule.CustomerScheduleUpdateDateAndTime(customerSchedule);

            /*var doctorSchedule = new WorkingSchedule
            {
                Id = Guid.NewGuid(),
                CustomerScheduleId = customerSchedule.Id,
                DoctorClinicId = customerSchedule.DoctorId,
                StartTime = request.StartTime,
                EndTime = endTime,
                Date = customerSchedule.Date.Value,
            };*/
            var doctorSchedule =
                await workingScheduleRepositoryBase.FindSingleAsync(x => x.CustomerScheduleId == customerSchedule.Id,
                    cancellationToken);
            if (doctorSchedule == null)
                return Result.Failure(new Error("404", "Doctor Schedule Not Found !"));
            doctorSchedule.StartTime = request.StartTime;
            doctorSchedule.EndTime = endTime;
            doctorSchedule.Date = customerSchedule.Date.Value;
            workingScheduleRepositoryBase.Update(doctorSchedule);
            doctorSchedule.WorkingScheduleUpdate([doctorSchedule],
                customerSchedule.Doctor.User.FirstName + " " + customerSchedule.Doctor.User.LastName);
            mailService.SendMail(new MailContent
            {
                To = customerSchedule.Customer.Email,
                Subject = "Yêu cầu lịch hẹn của quý khách vừa được cập nhập",
                Body =
                    $"Lịch của quý khách hàng {customerSchedule.Customer.FullName} đã được cập nhật ngày {customerSchedule.Date} vào lúc {customerSchedule.StartTime.ToString()}"
            });
        }

        return Result.Success();
    }
}