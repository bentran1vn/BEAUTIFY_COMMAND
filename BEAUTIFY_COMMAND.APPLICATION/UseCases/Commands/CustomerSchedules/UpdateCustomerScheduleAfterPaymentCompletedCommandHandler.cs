﻿using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class UpdateCustomerScheduleAfterPaymentCompletedCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> repositoryBase,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepositoryBase) : ICommandHandler<
    Command.UpdateCustomerScheduleAfterPaymentCompletedCommand>
{
    public async Task<Result> Handle(Command.UpdateCustomerScheduleAfterPaymentCompletedCommand request,
        CancellationToken cancellationToken)
    {
        var list = new List<string>
        {
            Constant.OrderStatus.ORDER_COMPLETED,
            Constant.OrderStatus.ORDER_IN_PROGRESS
        };
        if (!list.Contains(request.Status))
            return Result.Failure(new Error("400", "Status is not valid"));
        var customerSchedule = await repositoryBase.FindByIdAsync(request.CustomerScheduleId, cancellationToken);
        if (customerSchedule == null)
            return Result.Failure(new Error("404", "Customer Schedule Not Found"));
        if (customerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Customer Schedule already completed"));
        customerSchedule.Status = request.Status;

        repositoryBase.Update(customerSchedule);
        customerSchedule.UpdateCustomerScheduleStatus(customerSchedule.Id, request.Status);
        var workingSchedule = await workingScheduleRepositoryBase
            .FindSingleAsync(x => x.CustomerScheduleId == customerSchedule.Id, cancellationToken);
        workingSchedule?.UpdateDoctorScheduleStatus([workingSchedule.Id], request.Status);
        return Result.Success();
    }
}