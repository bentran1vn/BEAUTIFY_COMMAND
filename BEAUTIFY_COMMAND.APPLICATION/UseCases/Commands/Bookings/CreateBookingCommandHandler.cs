using BEAUTIFY_COMMAND.DOMAIN;
using BEAUTIFY_COMMAND.DOMAIN.Abstractions;
using BEAUTIFY_COMMAND.DOMAIN.MailTemplates;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Bookings;
/// <summary>
///     Handler for creating a new booking with distributed locking to prevent double-bookings
/// </summary>
/// <remarks>
///     API Route: POST api/v{version:apiVersion}/bookings
///     Authorization: Requires CUSTOMER role
///     Description: Creates a new booking for a service with a doctor at a clinic
/// </remarks>
internal sealed class

    #region DI

    CreateBookingCommandHandler(
        IRepositoryBase<User, Guid> userRepositoryBase,
        IRepositoryBase<Staff, Guid> staffRepositoryBase,
        IRepositoryBase<Clinic, Guid> clinicRepositoryBase,
        IRepositoryBase<UserClinic, Guid> userClinicRepositoryBase,
        ICurrentUserService currentUserService,
        IRepositoryBase<ProcedurePriceType, Guid> procedurePriceTypeRepositoryBase,
        IRepositoryBase<Order, Guid> orderRepositoryBase,
        IRepositoryBase<OrderDetail, Guid> orderDetailRepositoryBase,
        IRepositoryBase<Service, Guid> serviceRepositoryBase,
        IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepositoryBase,
        IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
        IMailService mailService,
        IRepositoryBase<Promotion, Guid> promotionRepositoryBase,
        IRepositoryBase<LivestreamRoom, Guid> livestreamRoomRepositoryBase,
        IDistributedLockService distributedLockService,
        IRepositoryBase<WalletTransaction, Guid> walletTransactionRepositoryBase
    )

    #endregion

    : ICommandHandler<CONTRACT.Services.Bookings.Commands.CreateBookingCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Bookings.Commands.CreateBookingCommand request,
        CancellationToken cancellationToken)
    {
        #region Validate Input

        var user = await userRepositoryBase.FindSingleAsync(x =>
            x.Id.Equals(currentUserService.UserId) && !x.IsDeleted, cancellationToken);
        if (user is null)
            return Result.Failure(new Error("404", "User Not Found !"));


        var doctor = await staffRepositoryBase.FindSingleAsync(x =>
            x.Id.Equals(request.DoctorId) && !x.IsDeleted, cancellationToken);
        if (doctor is null)
            return Result.Failure(new Error("404", "Doctor Not Found !"));
        if (!doctor.DoctorServices.Any(x => x.ServiceId.Equals(request.ServiceId)))
            return Result.Failure(new Error("400", "Doctor Cannot Perform This Service !"));


        var clinic = await clinicRepositoryBase.FindSingleAsync(x =>
            x.Id.Equals(request.ClinicId) && !x.IsDeleted, cancellationToken);
        if (clinic is null)
            return Result.Failure(new Error("404", "Clinic Not Found !"));
        if (!clinic.ClinicServices.Any(x => x.ServiceId.Equals(request.ServiceId)))
            return Result.Failure(new Error("400", "Clinic Cannot Perform This Service !"));


        var service = await serviceRepositoryBase.FindSingleAsync(x =>
            x.Id.Equals(request.ServiceId) && !x.IsDeleted, cancellationToken);
        if (service is null)
            return Result.Failure(new Error("404", "Service Not Found !"));


        var userClinic = await userClinicRepositoryBase.FindSingleAsync(
            x => x.UserId.Equals(request.DoctorId) && x.ClinicId.Equals(request.ClinicId) && !x.IsDeleted,
            cancellationToken);
        if (userClinic is null)
            return Result.Failure(new Error("404", "User Clinic Not Found !"));

        #endregion

        #region Distributed Locking

        // Create a unique lock key based on doctor, date, and time
        var lockKey = $"booking:{request.DoctorId}:{request.BookingDate}:{request.StartTime}";

        try
        {
            // Try to acquire a distributed lock with a 30-second expiry and 10-second wait time
            using var lockHandle = await distributedLockService.AcquireLockAsync(
                lockKey,
                TimeSpan.FromSeconds(30), // Lock expiry time
                TimeSpan.FromSeconds(10), // Wait time
                cancellationToken);


            #region Procedure Validation

            var query = procedurePriceTypeRepositoryBase.FindAll(x => !x.IsDeleted)
                .Select(x => new
                {
                    x.Id,
                    x.Price,
                    x.IsDefault,
                    x.Duration,
                    ProcedureServiceId = x.Procedure.ServiceId,
                    x.Procedure.StepIndex,
                    DiscountPrice = x.Procedure.Service.DiscountPrice ?? 0,
                    x.Procedure
                });

            // Join with a subquery to get min prices per StepIndex
            query = request.IsDefault
                ? query.Where(x => x.Procedure.ServiceId == service.Id && x.IsDefault)
                : query.Where(x => request.ProcedurePriceTypeIds.Contains(x.Id));

            var list = await query.ToListAsync(cancellationToken);

            if (list.Count == 0)
                return Result.Failure(new Error("404", "No valid procedures found."));

            var serviceIds = list.Select(x => x.ProcedureServiceId).Distinct().ToList();

            var stepIndexes = list.Select(x => x.Procedure.StepIndex).ToList();
            if (serviceIds.Count > 1 || stepIndexes.Count != stepIndexes.Distinct().Count())
                return Result.Failure(new Error("400",
                    "Conflicting procedures: Multiple services or overlapping steps."));

            var maxStepIndex = stepIndexes.Max();
            var expectedSteps = Enumerable.Range(1, maxStepIndex).ToList();
            if (!stepIndexes.OrderBy(x => x).SequenceEqual(expectedSteps))
                return Result.Failure(
                    new Error("400", "Step indexes are not in a valid sequence or steps are missing."));

            // Calculate duration of procedures before using it
            var initialProcedure = list.Where(x => x.StepIndex == 1).Select(x => x.Id).FirstOrDefault();
            //todo no hardcode 
            var durationOfProcedures =
                list.Where(x => x.StepIndex == 1).Select(x => x.Duration).FirstOrDefault() / 60.0 ;

            #endregion

            #region Availability Verification

            // Find a doctor shift that covers the requested booking time
            var doctorShift = await workingScheduleRepositoryBase.FindSingleAsync(x =>
                    x.Date == request.BookingDate &&
                    x.DoctorId == doctor.Id &&
                    x.ClinicId == clinic.Id &&
                    x.CustomerScheduleId == null && // This is a registered shift, not a booking
                    x.StartTime <= request.StartTime &&
                    x.EndTime > request.StartTime,
                cancellationToken);

            if (doctorShift == null)
                return Result.Failure(new Error("400", "Doctor is not scheduled to work at this time."));

            // Calculate the end time of the requested booking
            var requestEndTime = request.StartTime.Add(TimeSpan.FromHours(durationOfProcedures));

            // Ensure the entire booking duration fits within the doctor's shift
            if (requestEndTime > doctorShift.EndTime)
                return Result.Failure(new Error("400", "Booking duration exceeds doctor's shift end time."));

            // Check if the doctor already has a booking that conflicts with this time
            var existingBooking = await workingScheduleRepositoryBase.FindSingleAsync(x =>
                    x.Date == request.BookingDate &&
                    x.DoctorId == doctor.Id &&
                    x.ClinicId == clinic.Id &&
                    x.CustomerScheduleId != null && // This indicates a booking
                    (
                        (request.StartTime >= x.StartTime &&
                         request.StartTime < x.EndTime) || // New booking starts during existing booking
                        (requestEndTime > x.StartTime &&
                         requestEndTime <= x.EndTime) || // New booking ends during existing booking
                        (request.StartTime <= x.StartTime &&
                         requestEndTime >= x.EndTime) // New booking completely overlaps existing booking
                    ),
                cancellationToken);

            if (existingBooking != null)
                return Result.Failure(new Error("400", "Doctor already has a booking at this time."));

            #endregion

            #region Pricing and Discount Calculation

            decimal? discountPrice = 0;
            var total = list.Sum(x => x.Price);

            if (request.LiveStreamRoomId != null)
            {
                var livestreamRoom = await livestreamRoomRepositoryBase.FindSingleAsync(
                    x => x.Id == request.LiveStreamRoomId && x.Status == "live" && x.EndDate == null,
                    cancellationToken);
                if (livestreamRoom == null)
                    return Result.Failure(new Error("404", "Livestream room not found or not available"));

                var discount = await promotionRepositoryBase.FindSingleAsync(
                    x => x.ServiceId == request.ServiceId && x.IsActivated &&
                         x.LivestreamRoomId == request.LiveStreamRoomId,
                    cancellationToken);

                if (discount != null)
                    // Calculate the discounted price, not the discount amount
                    // discountPrice = total * (1 - (decimal)discount.DiscountPercent);
                    discountPrice = total * (decimal)discount.DiscountPercent;
                else
                    // If using service.DiscountPrice, make sure this is the final price after discount
                    // not the discount amount
                    discountPrice = service.DiscountPrice;
            }

            var deposit = service.DepositPercent;
            // Now discountPrice contains the final price after discount
            var depositAmount = Math.Round((total - discountPrice ?? 0) * (decimal)deposit / 100, 2);

            // Check if user has sufficient balance for deposit
            if (user.Balance < depositAmount)
                return Result.Failure(new Error("400", ErrorMessages.Wallet.InsufficientBalance +
                                                       $" Bạn cần tối thiểu {depositAmount} trong ví dùng để đặt cọc."));

            #endregion

            #region Entity Creation

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = user.Id,
                ServiceId = list.First().ProcedureServiceId,
                Status = Constant.OrderStatus.ORDER_PENDING,
                Discount = discountPrice,
                TotalAmount = total,
                DepositAmount = depositAmount,
                FinalAmount = total - discountPrice - depositAmount,
                LivestreamRoomId = request.LiveStreamRoomId
            };

            var orderDetails = list.Select(x => new OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProcedurePriceTypeId = x.Id,
                Price = x.Price
            }).ToList();

            var procedure = await procedurePriceTypeRepositoryBase.FindByIdAsync(initialProcedure, cancellationToken);

            var customerSchedule = new CustomerSchedule
            {
                Id = Guid.NewGuid(),
                CustomerId = currentUserService.UserId!.Value,
                DoctorId = userClinic.Id,
                ServiceId = request.ServiceId,
                OrderId = order.Id,
                StartTime = request.StartTime,
                EndTime = requestEndTime,
                Date = request.BookingDate,
                ProcedurePriceTypeId = initialProcedure,
                ProcedurePriceType = procedure,
                Status = Constant.OrderStatus.ORDER_PENDING,
                Procedure = procedure.Procedure
            };

            var doctorSchedule = new WorkingSchedule
            {
                Id = Guid.NewGuid(),
                CustomerScheduleId = customerSchedule.Id,
                DoctorId = doctor.Id,
                ClinicId = clinic.Id,
                StartTime = request.StartTime,
                EndTime = requestEndTime,
                Date = request.BookingDate,
                ShiftGroupId = doctorShift.ShiftGroupId // Maintain the shift group reference
            };

            #endregion

            #region Transaction Processing

            // Process the deposit directly instead of using the command bus
            // Create the wallet transaction for the deposit
            var depositTransaction = CreateDepositTransaction(
                user.Id,
                order.Id,
                depositAmount,
                $"Deposit for booking {order.Id} - {service.Name}"
            );

            // Deduct the deposit amount from the user's balance
            user.Balance -= depositAmount;
            userRepositoryBase.Update(user);

            #endregion

            #region Persistence

            // Save the transaction
            walletTransactionRepositoryBase.Add(depositTransaction);

            // Save all entities
            orderRepositoryBase.Add(order);
            orderDetailRepositoryBase.AddRange(orderDetails);
            workingScheduleRepositoryBase.Add(doctorSchedule);
            customerScheduleRepositoryBase.Add(customerSchedule);
            doctorSchedule.WorkingScheduleCreate(doctor.Id, clinic.Id, doctor.FirstName + " " + doctor.LastName,
                [doctorSchedule], customerSchedule);
            customerSchedule.Create(customerSchedule);

            #endregion

            #region Notification

            // Send booking confirmation email using the template
            await mailService.SendMail(
                BookingEmailTemplate.GetBookingConfirmationTemplate(
                    user,
                    order,
                    customerSchedule,
                    doctor,
                    clinic,
                    service,
                    depositAmount,
                    request.BookingDate,
                    request.StartTime
                )
            );

            #endregion


            return Result.Success(order.Id);
        }
        catch (TimeoutException ex)
        {
            return Result.Failure(new Error("409",
                "The booking time slot is currently being processed by another request. Please try again."));
        }

        #endregion
    }

    #region Helper Methods

    /// <summary>
    ///     Creates a new wallet transaction for the service booking deposit
    /// </summary>
    private static WalletTransaction CreateDepositTransaction(
        Guid userId,
        Guid orderId,
        decimal amount,
        string description)
    {
        // Get current time in Vietnam timezone
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var currentDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, vietnamTimeZone);

        return new WalletTransaction
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = amount,
            TransactionType = Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT,
            Status = Constant.WalletConstants.TransactionStatus.COMPLETED,
            IsMakeBySystem = true,
            Description = description,
            TransactionDate = currentDateTime,
            CreatedOnUtc = DateTimeOffset.UtcNow,
            OrderId = orderId
        };
    }

    #endregion
}