using System.Linq.Expressions;
using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class
    GenerateCustomerScheduleAfterPaymentCompletedCommandHandler(
        IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
        ICurrentUserService currentUserService,
        IRepositoryBase<Order, Guid> orderRepositoryBase) : ICommandHandler<
    Command.GenerateCustomerScheduleAfterPaymentCompletedCommand>
{
    public async Task<Result> Handle(Command.GenerateCustomerScheduleAfterPaymentCompletedCommand request,
        CancellationToken cancellationToken)
    {
        var userRole = currentUserService.Role;
        if (userRole != Constant.Role.CLINIC_STAFF && userRole != Constant.Role.CLINIC_ADMIN)
            return Result.Failure(new Error("403", "You do not have permission to access this resource."));
        Expression<Func<CustomerSchedule, object>>[] includes =
        [
            x => x.Customer,
            x => x.Service,
            x => x.Doctor,
            x => x.ProcedurePriceType
        ];
        var customerSchedule =
            await customerScheduleRepositoryBase.FindByIdAsync(request.CustomerScheduleId, cancellationToken, includes);
        if (customerSchedule == null)
            return Result.Failure(new Error("404", "Customer schedule not found."));
        if (customerSchedule.Status != Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Customer schedule is not completed."));
        var order = await orderRepositoryBase.FindSingleAsync(x => x.Id == customerSchedule.OrderId.Value,
            cancellationToken, x => x.OrderDetails!);
        if (order == null)
            return Result.Failure(new Error("404", "Customer order not found."));
        if (order.Status != Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Order is not completed."));
        var procedurePriceType = order.OrderDetails!.Select(x => x.ProcedurePriceTypeId).ToList();
        if (procedurePriceType.Count == 0)
            return Result.Failure(new Error("400", "Order does not have any procedure price type."));
        var customerScheduleList = procedurePriceType.Select(x => new CustomerSchedule
        {
            CustomerId = customerSchedule.CustomerId,
            ServiceId = customerSchedule.ServiceId,
            DoctorId = customerSchedule.DoctorId,
            StartTime = null,
            EndTime = null,
            Date = null,
            Status = Constant.OrderStatus.ORDER_PENDING,
            ProcedurePriceTypeId = x,
            OrderId = order.Id,
            DoctorNote = customerSchedule.DoctorNote, 
        }).ToList();
        customerScheduleRepositoryBase.AddRange(customerScheduleList);
        foreach (var x in customerScheduleList)
        {
            x.Create(x);
        }

        return Result.Success();
    }
}