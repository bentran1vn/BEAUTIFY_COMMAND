using System.Linq.Expressions;
using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class GenerateCustomerScheduleAfterPaymentCompletedCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    ICurrentUserService currentUserService,
    IRepositoryBase<Order, Guid> orderRepositoryBase)
    : ICommandHandler<Command.GenerateCustomerScheduleAfterPaymentCompletedCommand>
{
    private static readonly Expression<Func<CustomerSchedule, object>>[] Includes =
    [
        x => x.Customer,
        x => x.Service,
        x => x.Doctor,
        x => x.ProcedurePriceType,
        x => x.Order,
        x => x.ProcedurePriceType!.Procedure,
        x => x.ProcedurePriceType!.Procedure!.Service,
    ];

    public async Task<Result> Handle(Command.GenerateCustomerScheduleAfterPaymentCompletedCommand request,
        CancellationToken cancellationToken)
    {
        var customerSchedule = await GetValidCustomerScheduleAsync(request.CustomerScheduleId, cancellationToken);
        if (customerSchedule == null)
            return Result.Failure(new Error("404", "Customer schedule not found or not completed."));

        var order = await GetValidOrderAsync(customerSchedule.OrderId.Value, cancellationToken);
        if (order == null)
            return Result.Failure(new Error("404", "Customer order not found or not completed."));

        var procedurePriceTypes = GetProcedurePriceTypes(order);
        if (procedurePriceTypes.Count == 0)
            return Result.Failure(new Error("400", "No procedure price types found."));

        var newSchedules = CreateNewSchedules(customerSchedule, order, procedurePriceTypes);
        customerScheduleRepositoryBase.AddRange(newSchedules);
        newSchedules.ForEach(x => x.Create(x));

        return Result.Success();
    }


    private async Task<CustomerSchedule?> GetValidCustomerScheduleAsync(Guid scheduleId,
        CancellationToken cancellationToken)
    {
        var schedule = await customerScheduleRepositoryBase.FindByIdAsync(scheduleId, cancellationToken, Includes);
        return schedule is { Status: Constant.OrderStatus.ORDER_COMPLETED } ? schedule : null;
    }

    private async Task<Order?> GetValidOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await orderRepositoryBase.FindSingleAsync(
            x => x.Id == orderId,
            cancellationToken,
            x => x.OrderDetails!);
        return order is { Status: Constant.OrderStatus.ORDER_COMPLETED } ? order : null;
    }

    private static List<OrderDetail> GetProcedurePriceTypes(Order order) =>
        order.OrderDetails?
            .OrderBy(x => x.ProcedurePriceType.Procedure.StepIndex)
            .Skip(1)
            .ToList() ??[];

    private static List<CustomerSchedule> CreateNewSchedules(
        CustomerSchedule customerSchedule,
        Order order,
        List<OrderDetail> procedurePriceTypes) =>
        procedurePriceTypes.Select(x => new CustomerSchedule
        {
            CustomerId = customerSchedule.CustomerId,
            ServiceId = customerSchedule.ServiceId,
            DoctorId = customerSchedule.DoctorId,
            StartTime = null,
            EndTime = null,
            Date = null,
            Status = Constant.OrderStatus.ORDER_PENDING,
            ProcedurePriceTypeId = x.ProcedurePriceTypeId,
            OrderId = order.Id,
            DoctorNote = customerSchedule.DoctorNote,
            ProcedurePriceType = x.ProcedurePriceType,
        }).ToList();
}