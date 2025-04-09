namespace BEAUTIFY_COMMAND.CONTRACT.Services.DoctorServices;
public static class Commands
{
    //todo 1 serivce co nhieuu doctor
    public record DoctorSetWorkingServiceCommand(HashSet<Guid> DoctorId, Guid ServiceIds) : ICommand;

    public record DeleteDoctorServiceCommand(HashSet<Guid> DoctorServiceIds) : ICommand;
}