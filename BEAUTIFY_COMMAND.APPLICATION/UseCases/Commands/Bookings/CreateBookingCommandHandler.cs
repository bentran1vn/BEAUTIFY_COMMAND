using BEAUTIFY_COMMAND.DOMAIN;
using BEAUTIFY_COMMAND.DOMAIN.Abstractions;
using BEAUTIFY_COMMAND.DOMAIN.MailTemplates;

// Define a class to hold procedure data
internal class ProcedureData
{
    public Guid Id { get; init; }
    public decimal Price { get; init; }
    public bool IsDefault { get; init; }
    public int Duration { get; init; }
    public Guid ProcedureServiceId { get; init; }
    public int StepIndex { get; init; }
}

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Bookings
{
    /// <summary>
    /// Handler for creating a new booking with distributed locking to prevent double-bookings
    /// </summary>
    /// <remarks>
    /// API Route: POST api/v{version:apiVersion}/bookings
    /// Authorization: Requires CUSTOMER role
    /// Description: Creates a new booking for a service with a doctor at a clinic
    /// </remarks>
    internal sealed class CreateBookingCommandHandler(
        IRepositoryBase<User, Guid> userRepository,
        IRepositoryBase<Staff, Guid> staffRepository,
        IRepositoryBase<Clinic, Guid> clinicRepository,
        IRepositoryBase<UserClinic, Guid> userClinicRepository,
        ICurrentUserService currentUserService,
        IRepositoryBase<ProcedurePriceType, Guid> procedurePriceTypeRepository,
        IRepositoryBase<Order, Guid> orderRepository,
        IRepositoryBase<OrderDetail, Guid> orderDetailRepository,
        IRepositoryBase<Service, Guid> serviceRepository,
        IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepository,
        IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepository,
        IMailService mailService,
        IRepositoryBase<Promotion, Guid> promotionRepository,
        IRepositoryBase<LivestreamRoom, Guid> livestreamRoomRepository,
        IDistributedLockService distributedLockService,
        IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository)
        : ICommandHandler<CONTRACT.Services.Bookings.Commands.CreateBookingCommand>
    {
        /// <summary>
        /// Handles the creation of a new booking with distributed locking to prevent double-bookings
        /// </summary>
        /// <param name="request">The booking request details</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result containing the success status or error details</returns>
        public async Task<Result> Handle(
            CONTRACT.Services.Bookings.Commands.CreateBookingCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Get required entities in a single method
                var validationResult = await ValidateBookingRequest(request, cancellationToken);
                if (!validationResult.IsSuccess)
                    return validationResult.Result;

                var (user, doctor, clinic, service, userClinic) = validationResult.Entities;

                // Create a unique lock key based on doctor, date, and time with improved format
                var lockKey =
                    $"booking:{request.DoctorId}:{request.BookingDate.ToString("yyyy-MM-dd")}:{request.StartTime:hh\\:mm}";

                try
                {
                    // Try to acquire a distributed lock with optimized timeout settings
                    using var lockHandle = await distributedLockService.AcquireLockAsync(
                        lockKey,
                        TimeSpan.FromSeconds(15), // Reduced lock expiry time for better throughput
                        TimeSpan.FromSeconds(5), // Reduced wait time
                        cancellationToken);

                    // Validate procedures and calculate duration
                    var procedureResult = await ValidateProcedures(request, service.Id, cancellationToken);
                    if (!procedureResult.IsSuccess)
                        return procedureResult.Result;

                    var (procedureList, initialProcedureId, durationOfProcedures) = procedureResult.Data;

                    // Verify doctor availability
                    var availabilityResult = await VerifyDoctorAvailability(
                        request.BookingDate,
                        request.StartTime,
                        durationOfProcedures,
                        doctor.Id,
                        clinic.Id,
                        cancellationToken);

                    if (!availabilityResult.IsSuccess)
                        return availabilityResult.Result;

                    var doctorShift = availabilityResult.Data;

                    // Calculate pricing and discounts
                    var pricingResult = await CalculatePricing(
                        procedureList,
                        request.LiveStreamRoomId,
                        request.ServiceId,
                        service,
                        user.Balance,
                        cancellationToken);

                    if (!pricingResult.IsSuccess)
                        return pricingResult.Result;

                    var (total, discountAmount, depositAmount) = pricingResult.Data;

                    // Calculate end time for the booking
                    var requestEndTime = request.StartTime.Add(TimeSpan.FromHours(durationOfProcedures));

                    // Create entities and process transaction in one operation
                    var bookingResult = await CreateBookingEntities(
                        user,
                        doctor,
                        clinic,
                        service,
                        userClinic,
                        request,
                        procedureList,
                        initialProcedureId,
                        total,
                        discountAmount,
                        depositAmount,
                        requestEndTime,
                        doctorShift,
                        cancellationToken);

                    return bookingResult;
                }
                catch (TimeoutException)
                {
                    return Result.Failure(new Error("409",
                        "The booking time slot is currently being processed by another request. Please try again in a moment."));
                }
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains(
                                                           "concurrently using the same instance of DbContext"))
            {
                // Special handling for DbContext concurrency issues
                return Result.Failure(new Error("500",
                    "A database concurrency error occurred. Please try again after a few moments."));
            }
            catch (Exception ex)
            {
                // General exception handling
                return Result.Failure(new Error("500", $"An unexpected error occurred: {ex.Message}"));
            }
        }

        #region Helper Methods

        /// <summary>
        /// Validates all the required entities for the booking request
        /// </summary>
        private async Task<(bool IsSuccess, Result Result, (User user, Staff doctor, Clinic clinic, Service service,
                UserClinic userClinic) Entities)>
            ValidateBookingRequest(
                CONTRACT.Services.Bookings.Commands.CreateBookingCommand request,
                CancellationToken cancellationToken)
        {
            // Get entities sequentially to avoid DbContext concurrency issues
            var user = await userRepository.FindSingleAsync(x =>
                x.Id.Equals(currentUserService.UserId) && !x.IsDeleted, cancellationToken);

            if (user is null)
                return (false, Result.Failure(new Error("404", "User not found")), default);

            var doctor = await staffRepository.FindSingleAsync(x =>
                x.Id.Equals(request.DoctorId) && !x.IsDeleted, cancellationToken);

            if (doctor is null)
                return (false, Result.Failure(new Error("404", "Doctor not found")), default);

            var clinic = await clinicRepository.FindSingleAsync(x =>
                x.Id.Equals(request.ClinicId) && !x.IsDeleted, cancellationToken);

            if (clinic is null)
                return (false, Result.Failure(new Error("404", "Clinic not found")), default);

            var service = await serviceRepository.FindSingleAsync(x =>
                x.Id.Equals(request.ServiceId) && !x.IsDeleted, cancellationToken);

            if (service is null)
                return (false, Result.Failure(new Error("404", "Service not found")), default);

            // Check if doctor can perform the service
            if (!doctor.DoctorServices!.Any(x => x.ServiceId.Equals(request.ServiceId)))
                return (false, Result.Failure(new Error("400", "Doctor cannot perform this service")), default);

            // Check if clinic offers the service
            if (!clinic.ClinicServices!.Any(x => x.ServiceId.Equals(request.ServiceId)))
                return (false, Result.Failure(new Error("400", "Clinic does not offer this service")), default);

            // Check user-clinic relationship
            var userClinic = await userClinicRepository.FindSingleAsync(
                x => x.UserId.Equals(request.DoctorId) && x.ClinicId.Equals(request.ClinicId) && !x.IsDeleted,
                cancellationToken);

            if (userClinic is null)
                return (false, Result.Failure(new Error("404", "Doctor is not associated with this clinic")), default);

            return (true, null, (user, doctor, clinic, service, userClinic))!;
        }

        /// <summary>
        /// Validates and processes the procedures for the booking
        /// </summary>
        private async Task<(bool IsSuccess, Result Result, (List<ProcedureData> ProcedureList, Guid InitialProcedureId,
                double Duration) Data)>
            ValidateProcedures(
                CONTRACT.Services.Bookings.Commands.CreateBookingCommand request,
                Guid serviceId,
                CancellationToken cancellationToken)
        {
            // Define a class to hold procedure data
            var query = procedurePriceTypeRepository.FindAll(x => !x.IsDeleted)
                .Select(x => new ProcedureData
                {
                    Id = x.Id,
                    Price = x.Price,
                    IsDefault = x.IsDefault,
                    Duration = x.Duration,
                    ProcedureServiceId = x.Procedure.ServiceId!.Value,
                    StepIndex = x.Procedure.StepIndex
                });

            // Apply filters based on request
            query = request.IsDefault
                ? query.Where(x => x.ProcedureServiceId == serviceId && x.IsDefault)
                : query.Where(x => request.ProcedurePriceTypeIds.Contains(x.Id));

            var list = await query.ToListAsync(cancellationToken);

            if (list.Count == 0)
                return (false, Result.Failure(new Error("404", "No valid procedures found")), default);

            // Validation for service IDs and step indexes
            var serviceIds = list.Select(x => x.ProcedureServiceId).Distinct().ToList();
            var stepIndexes = list.Select(x => x.StepIndex).ToList();

            if (serviceIds.Count > 1)
                return (false, Result.Failure(new Error("400", "Procedures must be for the same service")), default);

            if (stepIndexes.Count != stepIndexes.Distinct().Count())
                return (false, Result.Failure(new Error("400", "Each procedure must have a unique step index")),
                    default);

            // Validate step sequence
            var maxStepIndex = stepIndexes.Max();
            var expectedSteps = Enumerable.Range(1, maxStepIndex).ToList();

            if (!stepIndexes.OrderBy(x => x).SequenceEqual(expectedSteps))
                return (false, Result.Failure(new Error("400", "Procedure steps must be in a sequential order")),
                    default);

            // Get initial procedure and duration
            var initialProcedureId = list.Where(x => x.StepIndex == 1).Select(x => x.Id).FirstOrDefault();
            var durationHours = list.Where(x => x.StepIndex == 1).Select(x => x.Duration).FirstOrDefault() / 60.0;

            return (true, null, (list, initialProcedureId, durationHours))!;
        }

        /// <summary>
        /// Verifies the doctor's availability for the requested booking time
        /// </summary>
        private async Task<(bool IsSuccess, Result Result, WorkingSchedule Data)>
            VerifyDoctorAvailability(
                DateOnly bookingDate,
                TimeSpan startTime,
                double durationHours,
                Guid doctorId,
                Guid clinicId,
                CancellationToken cancellationToken)
        {
            // Calculate the end time
            var endTime = startTime.Add(TimeSpan.FromHours(durationHours));

            // Find a matching shift for the doctor
            var doctorShift = await workingScheduleRepository.FindSingleAsync(x =>
                    x.Date == bookingDate &&
                    x.DoctorId == doctorId &&
                    x.ClinicId == clinicId &&
                    x.CustomerScheduleId == null && // This is a registered shift, not a booking
                    x.StartTime <= startTime &&
                    x.EndTime > startTime,
                cancellationToken);

            if (doctorShift == null)
                return (false, Result.Failure(new Error("400", "Doctor is not scheduled to work at this time")), null)!;

            // Check if booking fits in the shift
            if (endTime > doctorShift.EndTime)
                return (false, Result.Failure(new Error("400", "Booking duration exceeds doctor's shift end time")),
                    null)!;

            // Check for overlapping bookings
            var existingBooking = await workingScheduleRepository.FindSingleAsync(x =>
                    x.Date == bookingDate &&
                    x.DoctorId == doctorId &&
                    x.ClinicId == clinicId &&
                    x.CustomerScheduleId != null && // This is a booking
                    (
                        (startTime >= x.StartTime && startTime < x.EndTime) || // New booking starts during existing
                        (endTime > x.StartTime && endTime <= x.EndTime) || // New booking ends during existing
                        (startTime <= x.StartTime && endTime >= x.EndTime) // New booking encompasses existing
                    ),
                cancellationToken);

            if (existingBooking != null)
                return (false, Result.Failure(new Error("400", "Doctor already has a booking at this time")), null)!;

            return (true, null, doctorShift)!;
        }

        /// <summary>
        /// Calculates pricing, discounts, and deposit for the booking
        /// </summary>
        private async Task<(bool IsSuccess, Result Result, (decimal Total, decimal? DiscountAmount, decimal
                DepositAmount)
                Data)>
            CalculatePricing(
                List<ProcedureData> procedureList,
                Guid? livestreamRoomId,
                Guid serviceId,
                Service service,
                decimal userBalance,
                CancellationToken cancellationToken)
        {
            decimal? discountAmount = 0;
            var total = procedureList.Sum(x => x.Price);

            // Apply livestream discount if applicable
            if (livestreamRoomId.HasValue)
            {
                var livestreamRoom = await livestreamRoomRepository.FindSingleAsync(
                    x => x.Id == livestreamRoomId && x.Status == "live" && x.EndDate == null,
                    cancellationToken);

                if (livestreamRoom == null)
                    return (false, Result.Failure(new Error("404", "Livestream room not found or not available")),
                        default);

                var discount = await promotionRepository.FindSingleAsync(
                    x => x.ServiceId == serviceId && x.IsActivated && x.LivestreamRoomId == livestreamRoomId,
                    cancellationToken);

                if (discount != null)
                    discountAmount = total * (decimal)discount.DiscountPercent;
                else if (service.DiscountPrice.HasValue)
                    discountAmount = service.DiscountPrice;
            }

            // Calculate deposit
            var deposit = service.DepositPercent;
            var depositAmount = Math.Round((total - (discountAmount ?? 0)) * (decimal)deposit / 100, 2);

            // Check wallet balance
            if (userBalance < depositAmount)
                return (false, Result.Failure(new Error("400",
                        $"{ErrorMessages.Wallet.InsufficientBalance} Bạn cần tối thiểu {depositAmount} trong ví dùng để đặt cọc.")),
                    default);

            return (true, null, (total, discountAmount, depositAmount))!;
        }

        /// <summary>
        /// Creates all the necessary entities for a booking and processes the transaction
        /// </summary>
        private async Task<Result> CreateBookingEntities(
            User user,
            Staff doctor,
            Clinic clinic,
            Service service,
            UserClinic userClinic,
            CONTRACT.Services.Bookings.Commands.CreateBookingCommand request,
            List<ProcedureData> procedureList,
            Guid initialProcedureId,
            decimal total,
            decimal? discountAmount,
            decimal depositAmount,
            TimeSpan requestEndTime,
            WorkingSchedule doctorShift,
            CancellationToken cancellationToken)
        {
            // Create order entity
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = user.Id,
                ServiceId = procedureList.First().ProcedureServiceId,
                Status = Constant.OrderStatus.ORDER_PENDING,
                Discount = discountAmount,
                TotalAmount = total,
                DepositAmount = depositAmount,
                FinalAmount = total - (discountAmount ?? 0) - depositAmount,
                LivestreamRoomId = request.LiveStreamRoomId
            };

            // Create order details
            var orderDetails = procedureList.Select(x => new OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProcedurePriceTypeId = Guid.Parse(x.Id.ToString()),
                Price = x.Price
            }).ToList();

            // Get procedure for customer schedule
            var procedure = await procedurePriceTypeRepository.FindByIdAsync(initialProcedureId, cancellationToken);

            // Create customer schedule
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
                ProcedurePriceTypeId = initialProcedureId,
                ProcedurePriceType = procedure,
                Status = Constant.OrderStatus.ORDER_PENDING,
                Procedure = procedure!.Procedure
            };

            // Create doctor schedule
            var doctorSchedule = new WorkingSchedule
            {
                Id = Guid.NewGuid(),
                CustomerScheduleId = customerSchedule.Id,
                DoctorId = doctor.Id,
                ClinicId = clinic.Id,
                StartTime = request.StartTime,
                EndTime = requestEndTime,
                Date = request.BookingDate,
                ShiftGroupId = doctorShift.ShiftGroupId
            };

            // Create deposit transaction
            var depositTransaction = CreateDepositTransaction(
                user.Id,
                order.Id,
                depositAmount,
                $"Deposit for booking {order.Id} - {service.Name}"
            );

            try
            {
                // Process the transaction
                user.Balance -= depositAmount;
                clinic.Balance += depositAmount;

                // Update repositories
                userRepository.Update(user);
                walletTransactionRepository.Add(depositTransaction);
                orderRepository.Add(order);
                orderDetailRepository.AddRange(orderDetails);
                workingScheduleRepository.Add(doctorSchedule);
                customerScheduleRepository.Add(customerSchedule);

                // Call entity methods
                doctorSchedule.WorkingScheduleCreate(
                    doctor.Id,
                    clinic.Id,
                    $"{doctor.FirstName} {doctor.LastName}",
                    [doctorSchedule],
                    customerSchedule);

                customerSchedule.Create(customerSchedule);

                // Send email notification asynchronously (don't await)
                _ = mailService.SendMail(
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

                return Result.Success(order.Id);
            }
            catch (Exception ex)
            {
                // Log exception details
                return Result.Failure(new Error("500", $"Failed to create booking: {ex.Message}"));
            }
        }

        /// <summary>
        /// Creates a new wallet transaction for the service booking deposit
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
}