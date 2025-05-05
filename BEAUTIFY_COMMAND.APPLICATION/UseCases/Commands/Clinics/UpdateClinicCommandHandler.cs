namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
public class UpdateClinicCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<TriggerOutbox, Guid> triggerRepository,
    IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.UpdateClinicCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.UpdateClinicCommand request,
        CancellationToken cancellationToken)
    {
        var clinic = await clinicRepository.FindByIdAsync(new Guid(request.ClinicId), cancellationToken);

        if (clinic == null || clinic.IsDeleted) return Result.Failure(new Error("404", "Clinic not found."));

        if (request.Name != null) clinic.Name = request.Name;

        if (request.PhoneNumber != null) clinic.PhoneNumber = request.PhoneNumber;

        if (request.City != null) clinic.City = request.City;

        if (request.District != null) clinic.District = request.District;

        if (request.Ward != null) clinic.Ward = request.Ward;

        if (request.Address != null) clinic.Address = request.Address;

        if (request.WorkingTimeStart != null)
        {
            var startTime = TimeSpan.TryParse(request.WorkingTimeStart, out var time);
            if (!startTime)
                return Result.Failure(new Error("400", "Invalid working time start format."));
            clinic.WorkingTimeStart = time;
        }

        if (request.WorkingTimeEnd != null)
        {
            var endTime = TimeSpan.TryParse(request.WorkingTimeEnd, out var time);
            if (!endTime)
                return Result.Failure(new Error("400", "Invalid working time end format."));
            clinic.WorkingTimeEnd = time;
        }

        if (request.ProfilePicture != null)
        {
            var url = await mediaService.UploadImageAsync(request.ProfilePicture);
            clinic.ProfilePictureUrl = url;
        }

        if (request.IsActivated != null) clinic.IsActivated = request.IsActivated.Value;
        if (request.BankName != null) clinic.BankName = request.BankName;
        if (request.BankAccountNumber != null) clinic.BankAccountNumber = request.BankAccountNumber;

        var trigger = TriggerOutbox.UpdateBranchEvent(true, clinic);
        triggerRepository.Add(trigger);

        return Result.Success("Clinic updated.");
    }
}