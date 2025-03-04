using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Orders;
internal sealed class
    CustomerOrderServiceCommandHandler(
        IRepositoryBase<Service, Guid> serviceRepositoryBase,
        IRepositoryBase<User, Guid> userRepositoryBase,
        ICurrentUserService currentUserService,
        IRepositoryBase<ProcedurePriceType, Guid> procedurePriceTypeRepositoryBase,
        IRepositoryBase<Order, Guid> orderRepositoryBase,
        IRepositoryBase<OrderDetail, Guid> orderDetailRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Orders.Commands.CustomerOrderServiceCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Orders.Commands.CustomerOrderServiceCommand request,
        CancellationToken cancellationToken)
    {
        var currentUserId = currentUserService.UserId;

        var user = await userRepositoryBase.FindByIdAsync(currentUserId!.Value, cancellationToken) ??
                   throw new UserException.UserNotFoundException(currentUserId.Value);

        var procedures = await procedurePriceTypeRepositoryBase
            .FindAll(x => request.ProcedureIds.Contains(x.Id))
            .Include(x => x.Procedure) // Ensure we fetch related Procedure details
            .ThenInclude(p => p.Service)
            .ToListAsync(cancellationToken);

        if (procedures.Count == 0)
        {
            return Result.Failure(new Error("422", "No valid procedures found."));
        }

        // Extract ServiceIds from procedures
        var serviceIds = procedures.Select(x => x.Procedure.ServiceId).Distinct().ToList();

        if (serviceIds.Count > 1)
        {
            return Result.Failure(new Error("422",
                "Selected procedures belong to multiple services. Please choose procedures from the same service."));
        }

        // Check for overlapping step indexes
        var stepIndexes = procedures.Select(x => x.Procedure.StepIndex).ToList();
        if (stepIndexes.Count != stepIndexes.Distinct().Count())
        {
            return Result.Failure(new Error("422",
                "Procedures have overlapping step indexes. Each step index must be unique."));
        }

        var discount = procedures[0]!.Procedure!.Service!.DiscountPrice ?? 0;
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = user.Id,
            Status = Constant.ORDER_PENDING,
            Discount = discount,
        };
        //check if service discount is not null then apply discount

        var orderDetails = procedures.Select(x => new OrderDetail
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            ProcedurePriceTypeId = x.Id,
            Price = x.Price,
        }).ToList();
        order.TotalAmount = orderDetails.Sum(x => x.Price);
        order.FinalAmount = order.TotalAmount - discount;
        orderRepositoryBase.Add(order);
        orderDetailRepositoryBase.AddRange(orderDetails);
        var qrUrl =
            $"https://qr.sepay.vn/img?bank=MBBank&acc=0901928382&template=&amount={(int)order!.FinalAmount!}&des=CustomerOrder{order.Id}";
        var result = new
        {
            TransactionId = order.Id,
            BankNumber = "100879223979",
            BankGateway = "VietinBank",
            order.FinalAmount,
            OrderDescription = $"Customer Order: {order.Id}",
            QrUrl = qrUrl,
        };
        return Result.Success(result);
    }
}