namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Feedbacks;

public class CreateFeedbackCommandHandler : ICommandHandler<CONTRACT.Services.Feedbacks.Commands.CreateFeedbackCommand>
{
    private readonly IRepositoryBase<Feedback, Guid> _scheduleFeedbackRepository;
    private readonly IRepositoryBase<OrderFeedback, Guid> _orderFeedbackRepository;
    private readonly IRepositoryBase<Order, Guid> _orderRepository;
    private readonly IRepositoryBase<CustomerSchedule, Guid> _customerScheduleRepository;

    public CreateFeedbackCommandHandler(IRepositoryBase<Feedback, Guid> scheduleFeedbackRepository, IRepositoryBase<OrderFeedback, Guid> orderFeedbackRepository, IRepositoryBase<Order, Guid> orderRepository, IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepository)
    {
        _scheduleFeedbackRepository = scheduleFeedbackRepository;
        _orderFeedbackRepository = orderFeedbackRepository;
        _orderRepository = orderRepository;
        _customerScheduleRepository = customerScheduleRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Feedbacks.Commands.CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository
            .FindAll(x => x.Id.Equals(request.OrderId)).FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return Result.Failure(new Error("404", "Order not found"));
        }
        
        var customerSchedule = await _customerScheduleRepository
                .FindAll(x => x.OrderId.Equals(request.OrderId))
                .ToListAsync(cancellationToken);
        
        if (customerSchedule == null || !customerSchedule.Any())
        {
            return Result.Failure(new Error("404", "Customer schedule not found"));
        }
        
        if(customerSchedule.Any(x => x.Status != Constant.OrderStatus.ORDER_COMPLETED))
        {
            return Result.Failure(new Error("400", "Customer schedule not completed"));
        }

        if (customerSchedule.Count != request.ScheduleFeedbacks.Count)
        {
            return Result.Failure(new Error("500", "Missing schedule feedback"));
        }
        
        
        
        throw new NotImplementedException();
    }
}