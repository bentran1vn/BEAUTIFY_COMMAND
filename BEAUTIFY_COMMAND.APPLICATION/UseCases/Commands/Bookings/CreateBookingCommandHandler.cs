using System.Globalization;
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
        IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Bookings.Commands.CreateBookingCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Bookings.Commands.CreateBookingCommand request,
        CancellationToken cancellationToken)
    {
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

        var userClinic = await userClinicRepositoryBase.FindSingleAsync(x =>
                x.UserId.Equals(request.DoctorId) && x.ClinicId.Equals(request.ClinicId) && !x.IsDeleted,
            cancellationToken);
        if (userClinic is null)
            return Result.Failure(new Error("404", "User Clinic Not Found !"));
        var query = procedurePriceTypeRepositoryBase.FindAll(x =>
                request.ProcedurePriceTypeIds.Contains(x.Id) && !x.IsDeleted)
            .Select(x => new
            {
                x.Id,
                x.Price,
                x.IsDefault,
                x.Duration,
                ProcedureServiceId = x.Procedure.ServiceId,
                x.Procedure.StepIndex,
                x.Procedure.Service.DiscountPrice,
                x.Procedure
            });

        if (request.IsDefault)
        {
            query = query.Where(x => x.IsDefault);
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
        var durationOfProcedures = list.Sum(x => x.Duration) / 60.0;
        var initialProcedure = list.Where(x => x.StepIndex == 1).Select(x => x.Id).FirstOrDefault();

        var customerschedule = new CustomerSchedule
        {
            Id = Guid.NewGuid(),
            CustomerId = currentUserService.UserId!.Value,
            DoctorId = userClinic.Id,
            ServiceId = request.ServiceId,
            OrderId = order.Id,
            StartTime = request.StartTime,
            EndTime = request.StartTime.Add(TimeSpan.FromHours(durationOfProcedures)),
            Date = DateOnly.Parse(request.BookingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ProcedurePriceTypeId = initialProcedure,
            Status = Constant.OrderStatus.ORDER_PENDING,
        };
        var doctorSchedule = new WorkingSchedule
        {
            Id = Guid.NewGuid(),
            DoctorClinicId = userClinic.Id,
            StartTime = request.StartTime,
            EndTime = request.StartTime.Add(TimeSpan.FromHours(durationOfProcedures)),
            Date = DateOnly.Parse(request.BookingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
        };

        orderRepositoryBase.Add(order);
        orderDetailRepositoryBase.AddRange(orderDetails);
        workingScheduleRepositoryBase.Add(doctorSchedule);
        customerScheduleRepositoryBase.Add(customerschedule);
        doctorSchedule.WorkingScheduleCreate(doctor.Id, clinic.Id, doctor.FirstName + " " + doctor.LastName,
            [doctorSchedule]);
        return Result.Success();
    }
}