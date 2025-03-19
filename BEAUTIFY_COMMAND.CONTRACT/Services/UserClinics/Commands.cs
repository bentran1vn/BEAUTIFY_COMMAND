namespace BEAUTIFY_COMMAND.CONTRACT.Services.UserClinics;
public static class Commands
{
    public record ChangeDoctorToAnotherBranchCommand(Guid OldBranchId, Guid NewBranchId, Guid DoctorId) : ICommand;
}