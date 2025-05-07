using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
/// <summary>
/// Handler for changing a customer's doctor for schedules
/// </summary>
internal sealed class StaffChangeCustomerScheduleDoctorCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepositoryBase,
    IRepositoryBase<UserClinic, Guid> doctorRepositoryBase,
    ICurrentUserService currentUserService,
    IMailService mailService) : ICommandHandler<Command.StaffChangeCustomerScheduleDoctorCommand>
{
    public async Task<Result> Handle(Command.StaffChangeCustomerScheduleDoctorCommand request,
        CancellationToken cancellationToken)
    {
        // Retrieve the customer schedule to be updated
        var customerSchedule =
            await customerScheduleRepositoryBase.FindSingleAsync(x => x.Id == request.CustomerScheduleId,
                cancellationToken);

        // Return error if customer schedule doesn't exist
        if (customerSchedule is null)
            return Result.Failure(new Error("404", "Customer Schedule Not Found!"));

        // Check if the schedule is already completed
        if (customerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Cannot change doctor for a completed schedule!"));

        // Find the new doctor
        var newDoctor = await doctorRepositoryBase.FindSingleAsync(
            x => x.UserId == request.DoctorId &&
                 x.ClinicId == currentUserService.ClinicId,
            cancellationToken);

        if (newDoctor is null)
            return Result.Failure(new Error("404", "Doctor Not Found!"));

        // Check if the doctor is already booked at the requested time
        var workingSchedule = await workingScheduleRepositoryBase.FindAll(x =>
            x.Date == request.Date &&
            x.DoctorId == request.DoctorId &&
            x.ClinicId == newDoctor.ClinicId &&
            x.CustomerScheduleId != null &&
           ! x.IsDeleted&&
            x.StartTime == request.StartTime).ToListAsync(cancellationToken);

        if (workingSchedule.Any())
            return Result.Failure(new Error("400", "Doctor already has an appointment at this time!"));

        // Calculate end time based on procedure duration
        var endTime = request.StartTime.Add(
            TimeSpan.FromHours(customerSchedule.ProcedurePriceType.Duration / 60.0));

        // Check if the new doctor is available at the requested time
        var doctorBusy = await workingScheduleRepositoryBase.FindAll(x => x.Date == request.Date &&
                                                                          x.DoctorId == request.DoctorId &&
                                                                          x.CustomerScheduleId != null &&
                                                                          x.StartTime == request.StartTime)
            .AnyAsync(cancellationToken);

        if (doctorBusy)
            return Result.Failure(new Error("400", "Doctor is busy at this time!"));

        // Check for schedule overlaps with other appointments
        var overlapCheck = await CheckDoctorScheduleOverlap(
            workingScheduleRepositoryBase.FindAll(),
            request.Date,
            request.DoctorId,
            newDoctor.ClinicId,
            customerSchedule.Id,
            request.StartTime,
            endTime,
            cancellationToken);

        if (overlapCheck.IsFailure)
            return overlapCheck;

        // Validate the requested date is not in the past
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        if (request.Date < currentDate)
            return Result.Failure(new Error("400", "Cannot schedule appointments for past dates!"));

        if (request.IsSingleSchedule)
        {
            // Update only this specific schedule
            await UpdateSingleSchedule(
                customerSchedule,
                newDoctor,
                request.Date,
                request.StartTime,
                endTime,
                cancellationToken);
        }
        else
        {
            // Update all future schedules with the same order ID
            if (!customerSchedule.OrderId.HasValue)
                return Result.Failure(new Error("400", "This schedule is not part of a sequential order!"));

            await UpdateAllFutureSchedules(
                customerSchedule.OrderId.Value,
                customerSchedule.ProcedurePriceType.Procedure.StepIndex,
                newDoctor,
                cancellationToken);

            // Update and create working schedule only for the current appointment
            await UpdateCurrentScheduleWorkingSchedule(
                customerSchedule,
                newDoctor,
                request.Date,
                request.StartTime,
                endTime,
                cancellationToken);
        }

        return Result.Success();
    }

    /// <summary>
    /// Update a single customer schedule with a new doctor
    /// </summary>
    private async Task UpdateSingleSchedule(
        CustomerSchedule customerSchedule,
        UserClinic newDoctor,
        DateOnly date,
        TimeSpan startTime,
        TimeSpan endTime,
        CancellationToken cancellationToken)
    {
        // Update the customer schedule with the new doctor
        var oldDoctorId = customerSchedule.DoctorId;
        customerSchedule.DoctorId = newDoctor.Id;
        customerSchedule.Date = date;
        customerSchedule.StartTime = startTime;
        customerSchedule.EndTime = endTime;

        // Update the database
        customerScheduleRepositoryBase.Update(customerSchedule);
        customerSchedule.CustomerScheduleDoctorChanged([customerSchedule.Id], customerSchedule.DoctorId,
            customerSchedule.Doctor.User.FullName);

        // Find the old doctor's working schedule
        var oldWorkingSchedule = await workingScheduleRepositoryBase.FindSingleAsync(
            x => x.CustomerScheduleId == customerSchedule.Id,
            cancellationToken);

        if (oldWorkingSchedule != null)
        {
            // Delete the old working schedule
            workingScheduleRepositoryBase.Remove(oldWorkingSchedule);
        }

        // Create a new working schedule for the new doctor
        var newWorkingSchedule = new WorkingSchedule
        {
            Id = Guid.NewGuid(),
            CustomerScheduleId = customerSchedule.Id,
            DoctorId = newDoctor.UserId,
            ClinicId = newDoctor.ClinicId,
            StartTime = startTime,
            EndTime = endTime,
            Date = date,
            ShiftGroupId = oldWorkingSchedule?.ShiftGroupId
        };

        workingScheduleRepositoryBase.Add(newWorkingSchedule);

        // Raise domain events
        customerSchedule.CustomerScheduleUpdateDateAndTime(customerSchedule);
        newWorkingSchedule.WorkingScheduleCreate(
            newDoctor.UserId,
            newDoctor.ClinicId,
            $"{newDoctor.User?.FirstName} {newDoctor.User?.LastName}",
            [newWorkingSchedule],
            customerSchedule);

        // Send email notification to customer

        _ = mailService.SendMail(new MailContent
        {
            To = customerSchedule.Customer?.Email ?? "",
            Subject = "Your appointment has been updated with a new doctor",
            Body = $"Dear {customerSchedule.Customer?.FullName},\n\n" +
                   $"Your appointment on {customerSchedule.Date} at {customerSchedule.StartTime} " +
                   $"has been assigned to Dr. {newDoctor.User?.FirstName} {newDoctor.User?.LastName}."
        });
    }

    /// <summary>
    /// Update only the current schedule's working schedule for the new doctor
    /// </summary>
    private async Task UpdateCurrentScheduleWorkingSchedule(
        CustomerSchedule customerSchedule,
        UserClinic newDoctor,
        DateOnly date,
        TimeSpan startTime,
        TimeSpan endTime,
        CancellationToken cancellationToken)
    {
        try
        {
            // Update the customer schedule with the new doctor
            customerSchedule.DoctorId = newDoctor.Id;
            customerSchedule.Date = date;
            customerSchedule.StartTime = startTime;
            customerSchedule.EndTime = endTime;

            // Update the database
            customerScheduleRepositoryBase.Update(customerSchedule);

            // Find the old doctor's working schedule
            var oldWorkingSchedule = await workingScheduleRepositoryBase.FindSingleAsync(
                x => x.CustomerScheduleId == customerSchedule.Id,
                cancellationToken);

            if (oldWorkingSchedule != null)
            {
                // Delete the old working schedule
                workingScheduleRepositoryBase.Remove(oldWorkingSchedule);
            }

            // Create a new working schedule for the new doctor
            var newWorkingSchedule = new WorkingSchedule
            {
                Id = Guid.NewGuid(),
                CustomerScheduleId = customerSchedule.Id,
                DoctorId = newDoctor.UserId,
                ClinicId = newDoctor.ClinicId,
                StartTime = startTime,
                EndTime = endTime,
                Date = date,
                ShiftGroupId = oldWorkingSchedule?.ShiftGroupId
            };

            workingScheduleRepositoryBase.Add(newWorkingSchedule);

            // Raise domain events
            customerSchedule.CustomerScheduleUpdateDateAndTime(customerSchedule);
            newWorkingSchedule.WorkingScheduleCreate(
                newDoctor.UserId,
                newDoctor.ClinicId,
                $"{newDoctor.User?.FirstName} {newDoctor.User?.LastName}",
                [newWorkingSchedule],
                customerSchedule);

            // Send email notification to customer
            try
            {
                await mailService.SendMail(new MailContent
                {
                    To = customerSchedule.Customer?.Email ?? "",
                    Subject = "Your appointment has been updated with a new doctor",
                    Body = $"Dear {customerSchedule.Customer?.FullName},\n\n" +
                           $"Your appointment on {customerSchedule.Date} at {customerSchedule.StartTime} " +
                           $"has been assigned to Dr. {newDoctor.User?.FirstName} {newDoctor.User?.LastName}."
                });
            }
            catch (Exception)
            {
                // Log email failure but continue with the operation
                // The schedule update is successful even if email notification fails
            }
        }
        catch (Exception)
        {
            // Add proper transaction rollback or error handling here
            throw;
        }
    }

    /// <summary>
    /// Update all future schedules with the same order ID to have the new doctor ID
    /// This doesn't create new working schedules for future appointments
    /// </summary>
    private async Task UpdateAllFutureSchedules(
        Guid orderId,
        int currentStepIndex,
        UserClinic newDoctor,
        CancellationToken cancellationToken)
    {
        // Get all future schedules for this order that have not been completed
        var futureSchedules = await customerScheduleRepositoryBase.FindAll(x => x.OrderId == orderId &&
                x.Status != Constant.OrderStatus.ORDER_COMPLETED &&
                x.ProcedurePriceType.Procedure.StepIndex > currentStepIndex) // Only future schedules
            .ToListAsync(cancellationToken);

        // Simply update all future customer schedules to use the new doctor
        foreach (var schedule in futureSchedules)
        {
            // Update the customer schedule with the new doctor ID only
            schedule.DoctorId = newDoctor.Id;
            customerScheduleRepositoryBase.Update(schedule);
            schedule.CustomerScheduleDoctorChanged([schedule.Id], schedule.DoctorId,
                schedule.Doctor?.User!.FullName == null
                ? ""
                : schedule.Doctor.User.FullName);

            // Send email notification


            _ = mailService.SendMail(new MailContent
            {
                To = schedule.Customer?.Email ?? "",
                Subject = "Your future appointment has been updated with a new doctor",
                Body = $"Dear {schedule.Customer?.FullName},\n\n" +
                       $"Your appointment scheduled for {schedule.Date} " +
                       $"has been assigned to Dr. {newDoctor.User?.FirstName} {newDoctor.User?.LastName}."
            });
        }
    }

    /// <summary>
    /// Check if the proposed schedule overlaps with the doctor's existing appointments
    /// </summary>
    private async Task<Result> CheckDoctorScheduleOverlap(
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
                x.CustomerScheduleId != currentScheduleId &&
                x.CustomerScheduleId != null)
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