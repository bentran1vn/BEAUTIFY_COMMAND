namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.DoctorServices;
internal sealed class DeleteDoctorServiceCommandHandler(
    IRepositoryBase<DoctorService, Guid> doctorServiceRepository)
    : ICommandHandler<CONTRACT.Services.DoctorServices.Commands.DeleteDoctorServiceCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.DoctorServices.Commands.DeleteDoctorServiceCommand request,
        CancellationToken cancellationToken)
    {
        var doctorServicesToDelete = doctorServiceRepository
            .FindAll(ds => request.DoctorServiceIds.Contains(ds.Id))
            .ToList();

        if (doctorServicesToDelete.Count == 0)
        {
            return Result.Failure(new Error("404", "No matching doctor services found for deletion"));
        }

        // Delete the records
        doctorServiceRepository.RemoveMultiple(doctorServicesToDelete);
        doctorServicesToDelete[0]
            .RaiseDoctorServiceDeletedEvent(doctorServicesToDelete[0].ServiceId,doctorServicesToDelete.Select(x => x.Id).ToList());
        return Result.Success();
    }
}