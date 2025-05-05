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

        if (!string.IsNullOrEmpty(request.Name)) clinic.Name = request.Name;

        if (!string.IsNullOrEmpty(request.PhoneNumber)) clinic.PhoneNumber = request.PhoneNumber;

        if (!string.IsNullOrEmpty(request.City)) clinic.City = request.City;

        if (!string.IsNullOrEmpty(request.District)) clinic.District = request.District;

        if (!string.IsNullOrEmpty(request.Ward)) clinic.Ward = request.Ward;

        if (!string.IsNullOrEmpty(request.Address)) clinic.Address = request.Address;

        if (!string.IsNullOrEmpty(request.WorkingTimeStart))
        {
            var startTime = TimeSpan.TryParse(request.WorkingTimeStart, out var time);
            if (!startTime)
                return Result.Failure(new Error("400", "Invalid working time start format."));
            clinic.WorkingTimeStart = time;
        }

        if (!string.IsNullOrEmpty(request.WorkingTimeEnd))
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

        if (!string.IsNullOrEmpty(request.IsActivated)) clinic.IsActivated = bool.Parse(request.IsActivated);
        if (!string.IsNullOrEmpty(request.BankName)) clinic.BankName = request.BankName;
        if (!string.IsNullOrEmpty(request.BankAccountNumber)) clinic.BankAccountNumber = request.BankAccountNumber;

        var trigger = TriggerOutbox.UpdateBranchEvent(true, clinic);
        triggerRepository.Add(trigger);

        return Result.Success("Clinic updated.");
    }
}