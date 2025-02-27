using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.DoctorServices;
public static class Commands
{
    public record DoctorSetWorkingServiceCommand(Guid DoctorId, HashSet<Guid> ServiceIds) : ICommand;

    public record DeleteDoctorServiceCommand(HashSet<Guid> DoctorServiceIds) : ICommand;
}