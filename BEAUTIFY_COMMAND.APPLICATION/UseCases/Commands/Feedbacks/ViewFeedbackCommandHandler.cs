namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Feedbacks;

public class ViewFeedbackCommandHandler: ICommandHandler<CONTRACT.Services.Feedbacks.Commands.ViewFeedbackCommand>
{
    private readonly IRepositoryBase<OrderFeedback, Guid> _orderFeedbackRepository;
    private readonly IRepositoryBase<Order, Guid> _orderRepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxRepository;

    public ViewFeedbackCommandHandler(IRepositoryBase<OrderFeedback, Guid> orderFeedbackRepository, IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository, IRepositoryBase<Order, Guid> orderRepository)
    {
        _orderFeedbackRepository = orderFeedbackRepository;
        _triggerOutboxRepository = triggerOutboxRepository;
        _orderRepository = orderRepository;
    }
    
    public async Task<Result> Handle(CONTRACT.Services.Feedbacks.Commands.ViewFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await _orderFeedbackRepository
            .FindAll(x => x.Id.Equals(request.FeedbackId) && !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (feedback is null)
        {
            return Result.Failure(new Error("404", "Feedback not found"));
        }
        
        var order = await _orderRepository
            .FindSingleAsync(x => x.OrderFeedbackId.Equals(request.FeedbackId) && !x.IsDeleted, cancellationToken);
        
        var trigger = TriggerOutbox.DisplayFeedbackEvent(request.FeedbackId, order.CustomerId, request.IsDisplay);
        
        _triggerOutboxRepository.Add(trigger);

        return Result.Success("Change Display Feedback Success");
    }
}