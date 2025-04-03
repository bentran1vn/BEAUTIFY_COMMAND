using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class DoctorUpdateCustomerScheduleNoteCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase)
    : ICommandHandler<Command.DoctorUpdateCustomerScheduleNoteCommand>
{
    public async Task<Result> Handle(Command.DoctorUpdateCustomerScheduleNoteCommand request,
        CancellationToken cancellationToken)
    {
        var customerSchedule =
            await customerScheduleRepositoryBase.FindByIdAsync(request.CustomerScheduleId, cancellationToken);
        if (customerSchedule == null)
            return Result.Failure(new Error("404", "Customer Schedule Not Found"));
        customerSchedule.DoctorNote = request.DoctorNote;
        customerScheduleRepositoryBase.Update(customerSchedule);
        customerSchedule.UpdateCustomerScheduleNote(customerSchedule.Id, request.DoctorNote);
        return Result.Success();
    }
}