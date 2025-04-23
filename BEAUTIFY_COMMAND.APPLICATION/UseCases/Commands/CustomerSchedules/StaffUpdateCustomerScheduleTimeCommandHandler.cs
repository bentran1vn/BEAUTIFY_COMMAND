using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
/// <summary>
///  customer-schedules/staff
/// </summary>
/// <param name="customerScheduleRepositoryBase"></param>
/// <param name="workingScheduleRepositoryBase"></param>
/// <param name="mailService"></param>
internal sealed class StaffUpdateCustomerScheduleTimeCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepositoryBase,
    IMailService mailService) : ICommandHandler<
    Command.StaffUpdateCustomerScheduleTimeCommand>
{
    /// <summary>
    /// Handles the command to update a customer schedule time by staff.
    /// Supports updating both current and next schedules in a procedure sequence.
    /// </summary>
    /// <param name="request">The command containing schedule update information</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success result or error with explanation</returns>
    public async Task<Result> Handle(Command.StaffUpdateCustomerScheduleTimeCommand request,
        CancellationToken cancellationToken)
    {
        // Retrieve the customer schedule to be updated
        var customerSchedule =
            await customerScheduleRepositoryBase.FindSingleAsync(x => x.Id == request.CustomerScheduleId,
                cancellationToken);

        // Return error if customer schedule doesn't exist
        if (customerSchedule is null)
            return Result.Failure(new Error("404", "Customer Schedule Not Found !"));

        // Handle updating the next schedule in a procedure sequence
        if (request.IsNext)
        {
            // Find the next customer schedule in the sequence based on procedure step index
            var nextCustomerSchedule = await customerScheduleRepositoryBase.FindSingleAsync(
                x => x.OrderId == customerSchedule.OrderId && x.Status == Constant.OrderStatus.ORDER_PENDING &&
                     x.ProcedurePriceType.Procedure.StepIndex ==
                     customerSchedule.ProcedurePriceType.Procedure.StepIndex + 1, cancellationToken);

            // Return error if the next schedule doesn't exist
            if (nextCustomerSchedule == null)
                return Result.Failure(new Error("404", "Next Customer Schedule Not Found !"));

            // Check if the next schedule is already completed
            if (nextCustomerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
                return Result.Failure(new Error("400", "Next Customer Schedule Already Completed !"));

            // Check if the doctor is already booked at the requested time
            var workingSchedule = await
                workingScheduleRepositoryBase.FindAll(x =>
                    x.Date == request.Date &&
                    x.DoctorId == customerSchedule.Doctor.UserId &&
                    x.ClinicId == customerSchedule.Doctor.ClinicId &&
                    x.CustomerScheduleId != null &&
                    x.StartTime == request.StartTime).ToListAsync(cancellationToken);

            if (workingSchedule.Count != 0)
                return Result.Failure(new Error("400", "Doctor is busy at this time !"));

            // Update next customer schedule with new date and time
            nextCustomerSchedule.Date = request.Date;
            nextCustomerSchedule.StartTime = request.StartTime;


            // todo Calculate end time (procedure duration + 30 min buffer)
            var endTime = request.StartTime.Add(
                TimeSpan.FromHours(nextCustomerSchedule.ProcedurePriceType.Duration / 60.0 + 0.5));

            // Check for schedule overlaps with other appointments
            var overlapCheck = await CheckScheduleOverlap(
                workingScheduleRepositoryBase.FindAll(),
                request.Date,
                customerSchedule.Doctor.UserId,
                customerSchedule.Doctor.ClinicId,
                nextCustomerSchedule.Id,
                request.StartTime,
                endTime,
                cancellationToken);

            // Return error if scheduling conflict detected
            if (overlapCheck.IsFailure)
                return overlapCheck;

            // Update schedule end time and status
            nextCustomerSchedule.EndTime = endTime;
            nextCustomerSchedule.Status = Constant.OrderStatus.ORDER_PENDING;

            // Save changes to database
            customerScheduleRepositoryBase.Update(nextCustomerSchedule);
            nextCustomerSchedule.CustomerScheduleUpdateDateAndTime(nextCustomerSchedule);

            var workingScheduleId =
                await workingScheduleRepositoryBase.FindSingleAsync(x => x.CustomerScheduleId == customerSchedule.Id,
                    cancellationToken);
            // Create new doctor working schedule for this appointment
            var doctorSchedule = new WorkingSchedule
            {
                Id = Guid.NewGuid(),
                CustomerScheduleId = nextCustomerSchedule.Id,
                DoctorId = customerSchedule.Doctor.UserId,
                ClinicId = customerSchedule.Doctor.ClinicId,
                StartTime = request.StartTime,
                EndTime = endTime,
                Date = customerSchedule.Date.Value,
                ShiftGroupId = workingScheduleId.ShiftGroupId
            };
            workingScheduleRepositoryBase.Add(doctorSchedule);
            doctorSchedule.WorkingScheduleCreate(customerSchedule.Doctor.UserId, customerSchedule.Doctor.ClinicId,
                customerSchedule.Doctor.User.FirstName + " " + customerSchedule.Doctor.User.LastName,
                [doctorSchedule], nextCustomerSchedule);

            // Send email notification to customer about schedule update
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
            // Handle updating the current schedule

            // Check if current schedule is already completed
            if (customerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
                return Result.Failure(new Error("400", "Customer Schedule Already Completed !"));

            // Check if doctor is already booked at the requested time
            var workingSchedule = await
                workingScheduleRepositoryBase.FindAll(x =>
                    x.Date == request.Date && x.DoctorId == customerSchedule.Doctor.UserId &&
                    x.ClinicId == customerSchedule.Doctor.ClinicId &&
                    x.CustomerScheduleId != null &&
                    x.StartTime == request.StartTime).ToListAsync(cancellationToken);

            if (workingSchedule.Count != 0)
                return Result.Failure(new Error("400", "Doctor is busy at this time !"));

            // Update customer schedule with new date and time
            customerSchedule.Date = request.Date;
            customerSchedule.StartTime = request.StartTime;

            // todo Calculate end time (procedure duration + 30min buffer)
            var endTime = request.StartTime.Add(
                TimeSpan.FromHours(customerSchedule.ProcedurePriceType.Duration / 60.0 + 0.5));

            // Check for schedule overlaps with other appointments
            var overlapCheck = await CheckScheduleOverlap(
                workingScheduleRepositoryBase.FindAll(),
                request.Date,
                customerSchedule.Doctor.UserId,
                customerSchedule.Doctor.ClinicId,
                customerSchedule.Id,
                request.StartTime,
                endTime,
                cancellationToken);

            // Return error if scheduling conflict detected
            if (overlapCheck.IsFailure)
                return overlapCheck;

            // Update schedule end time and status
            customerSchedule.EndTime = endTime;
            customerSchedule.Status = Constant.OrderStatus.ORDER_PENDING;

            // Save changes to database
            customerScheduleRepositoryBase.Update(customerSchedule);
            customerSchedule.CustomerScheduleUpdateDateAndTime(customerSchedule);

            // Find and update corresponding doctor working schedule
            var doctorSchedule =
                await workingScheduleRepositoryBase.FindSingleAsync(x => x.CustomerScheduleId == customerSchedule.Id,
                    cancellationToken);
            if (doctorSchedule == null)
                return Result.Failure(new Error("404", "Doctor Schedule Not Found !"));

            // Update doctor's working schedule times
            doctorSchedule.StartTime = request.StartTime;
            doctorSchedule.EndTime = endTime;
            doctorSchedule.Date = customerSchedule.Date.Value;
            workingScheduleRepositoryBase.Update(doctorSchedule);
            doctorSchedule.WorkingScheduleUpdate([doctorSchedule],
                customerSchedule.Doctor.User.FirstName + " " + customerSchedule.Doctor.User.LastName);

            // Send email notification to customer about schedule update
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

    /// <summary>
    /// Helper method to check if a proposed schedule time overlaps with existing appointments
    /// or violates clinic operating hours.
    /// </summary>
    /// <param name="workingScheduleQuery">Query to retrieve working schedules</param>
    /// <param name="date">Proposed appointment date</param>
    /// <param name="doctorId">ID of the doctor</param>
    /// <param name="clinicId">ID of the clinic</param>
    /// <param name="currentScheduleId">ID of the current schedule (to exclude from overlap check)</param>
    /// <param name="startTime">Proposed start time</param>
    /// <param name="endTime">Calculated end time</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success result or error with explanation</returns>
    private async Task<Result> CheckScheduleOverlap(
        IQueryable<WorkingSchedule> workingScheduleQuery,
        DateOnly date,
        Guid doctorId,
        Guid clinicId,
        Guid currentScheduleId,
        TimeSpan startTime,
        TimeSpan endTime,
        CancellationToken cancellationToken)
    {
        // Get all other schedules for this doctor on this date (excluding the current one)
        var doctorSchedules = await workingScheduleQuery
            .Where(x =>
                x.Date == date &&
                x.DoctorId == doctorId &&
                x.ClinicId == clinicId &&
                x.CustomerScheduleId != currentScheduleId && x.CustomerScheduleId != null)
            .ToListAsync(cancellationToken);

        // Check for any overlaps with existing appointments
        if (doctorSchedules.Any(schedule => startTime < schedule.EndTime && endTime > schedule.StartTime))
        {
            return Result.Failure(new Error("400",
                "This appointment would overlap with another scheduled procedure."));
        }

      

        return Result.Success();
    }
}
