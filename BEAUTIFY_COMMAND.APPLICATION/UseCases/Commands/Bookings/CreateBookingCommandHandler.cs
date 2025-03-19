using System.Globalization;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Bookings;
internal sealed class
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
        IMailService mailService)
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

        var query = procedurePriceTypeRepositoryBase.FindAll(x => !x.IsDeleted)
            .Select(x => new
            {
                x.Id,
                x.Price,
                x.IsDefault,
                x.Duration,
                ProcedureServiceId = x.Procedure.ServiceId,
                StepIndex = x.Procedure.StepIndex,
                DiscountPrice = x.Procedure.Service.DiscountPrice ?? 0,
                Procedure = x.Procedure
            });

        if (request.IsDefault)
        {
            // Join with a subquery to get min prices per StepIndex
            var minPrices = query
                .GroupBy(x => x.StepIndex)
                .Select(g => new { StepIndex = g.Key, MinPrice = g.Min(x => x.Price) });

            query = from q in query
                join mp in minPrices on new { q.StepIndex, q.Price } equals new { mp.StepIndex, Price = mp.MinPrice }
                select q;
        }
        else
        {
            query = query.Where(x => request.ProcedurePriceTypeIds.Contains(x.Id));
        }

        var list = await query.ToListAsync(cancellationToken);

        if (list.Count == 0)
            return Result.Failure(new Error("404", "No valid procedures found."));

        var serviceIds = list.Select(x => x.ProcedureServiceId).Distinct().ToList();

        var stepIndexes = list.Select(x => x.Procedure.StepIndex).ToList();
        if (serviceIds.Count > 1 || stepIndexes.Count != stepIndexes.Distinct().Count())
        {
            return Result.Failure(new Error("400", "Conflicting procedures: Multiple services or overlapping steps."));
        }

        var maxStepIndex = stepIndexes.Max();
        var expectedSteps = Enumerable.Range(1, maxStepIndex).ToList();
        if (!stepIndexes.OrderBy(x => x).SequenceEqual(expectedSteps))
        {
            return Result.Failure(new Error("400", "Step indexes are not in a valid sequence or steps are missing."));
        }

        var discount = list.FirstOrDefault()?.DiscountPrice ?? 0;
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = user.Id,
            ServiceId = list.First().ProcedureServiceId,
            Status = Constant.OrderStatus.ORDER_PENDING,
            Discount = discount,
            TotalAmount = list.Sum(x => x.Price),
            FinalAmount = list.Sum(x => x.Price) - discount
        };
        var orderDetails = list.Select(x => new OrderDetail
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            ProcedurePriceTypeId = x.Id,
            Price = x.Price
        }).ToList();
        var durationOfProcedures = list.Sum(x => x.Duration) / 60.0 + 0.5;
        var initialProcedure = list.Where(x => x.StepIndex == 1).Select(x => x.Id).FirstOrDefault();
        var procedure = await procedurePriceTypeRepositoryBase.FindByIdAsync(initialProcedure, cancellationToken);

        var customerSchedule = new CustomerSchedule
        {
            Id = Guid.NewGuid(),
            CustomerId = currentUserService.UserId!.Value,
            DoctorId = userClinic.Id,
            ServiceId = request.ServiceId,
            OrderId = order.Id,
            StartTime = request.StartTime,
            EndTime = request.StartTime.Add(TimeSpan.FromHours(durationOfProcedures)),
            Date = request.BookingDate,
            ProcedurePriceTypeId = initialProcedure,
            ProcedurePriceType = procedure,
            Status = Constant.OrderStatus.ORDER_PENDING,
        };
        var doctorSchedule = new WorkingSchedule
        {
            Id = Guid.NewGuid(),
            DoctorClinicId = userClinic.Id,
            StartTime = request.StartTime,
            EndTime = request.StartTime.Add(TimeSpan.FromHours(durationOfProcedures)),
            Date = request.BookingDate,
        };

        orderRepositoryBase.Add(order);
        orderDetailRepositoryBase.AddRange(orderDetails);
        workingScheduleRepositoryBase.Add(doctorSchedule);
        customerScheduleRepositoryBase.Add(customerSchedule);
        doctorSchedule.WorkingScheduleCreate(doctor.Id, clinic.Id, doctor.FirstName + " " + doctor.LastName,
            [doctorSchedule]);
        customerSchedule.Create(customerSchedule);
        await mailService.SendMail(new MailContent
        {
            To = user.Email,
            Subject = "Booking Confirmation",
            Body = @"
    <html>
    <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
        <p>Dear " + user.FirstName + " " + user.LastName + @",</p>
        
        <p>Your booking has been successfully created. Here are the details:</p>
        
        <ul style=""list-style-type: none; padding: 0;"">
            <li><strong>Booking Date:</strong> " +
                   request.BookingDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + @"</li>
            <li><strong>Start Time:</strong> " + request.StartTime.ToString(@"hh\:mm") + @"</li>
            <li><strong>End Time:</strong> " + customerSchedule.EndTime.ToString(@"hh\:mm") + @"</li>
            <li><strong>Service:</strong> " + service.Name + @"</li>
            <li><strong>Doctor:</strong> " + doctor.FirstName + " " + doctor.LastName + @"</li>
            <li><strong>Address:</strong> " + clinic.Address + @"</li>
        </ul>
        
        <p>Thank you for choosing our service!</p>
        
        <p>When arrived at the clinic, please provide this email or provide to the staff with your full name and phone number.</p>
        
        <p>Best regards,</p>
        <p><strong>" + clinic.Name + @" Clinic</strong></p>
    </body>
    </html>
"
        });
        return Result.Success();
    }
}