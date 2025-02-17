namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.WorkingSchedules;
public class DeleteWorkingScheduleCommandHandler(IRepositoryBase<WorkingSchedule, Guid> workingRepository)
    : ICommandHandler<CONTRACT.Services.WorkingSchedules.Commands.DeleteWorkingScheduleCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.WorkingSchedules.Commands.DeleteWorkingScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var working =
            await workingRepository.FindSingleAsync(x => x.Id.Equals(request.WorkingScheduleId), cancellationToken);
        if (working is null) return Result.Failure(new Error("404", "Working Schedule not found"));
        workingRepository.Remove(working);
        working.WorkingScheduleDelete(request.WorkingScheduleId);
        return Result.Success();
    }
}