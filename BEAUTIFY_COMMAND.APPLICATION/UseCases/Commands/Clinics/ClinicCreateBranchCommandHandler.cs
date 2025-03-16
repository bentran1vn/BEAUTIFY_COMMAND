using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class
    ClinicCreateBranchCommandHandler(
        IRepositoryBase<Staff, Guid> staffRepository,
        IRepositoryBase<Clinic, Guid> clinicRepository,
        IMediaService mediaService,
        IPasswordHasherService passwordHasherService,
        IRepositoryBase<Role, Guid> roleRepository,
        ICurrentUserService currentUserService,
        IRepositoryBase<UserClinic, Guid> userClinicRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicCreateBranchCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicCreateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var parentClinic = await clinicRepository.FindByIdAsync(currentUserService.ClinicId.Value, cancellationToken) ??
                           throw new ClinicException.ClinicNotFoundException(currentUserService.ClinicId.Value);
        var role = await roleRepository.FindSingleAsync(x => x.Name == "Clinic Staff", cancellationToken);
        var OUrl = await mediaService.UploadImageAsync(request.OperatingLicense);
        var PUrl = await mediaService.UploadImageAsync(request.ProfilePictureUrl);
        var clinic = new Clinic
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            City = request.City,
            Ward = request.Ward,
            District = request.District,
            Address = request.Address,
            ParentId = currentUserService.ClinicId.Value,
            PhoneNumber = request.PhoneNumber,
            TaxCode = parentClinic.TaxCode,
            BusinessLicenseUrl = parentClinic.BusinessLicenseUrl,
            OperatingLicenseUrl = OUrl,
            OperatingLicenseExpiryDate = request.OperatingLicenseExpiryDate,
            Status = 1,
            BankName = request.BankName,
            BankAccountNumber = request.BankAccountNumber,
            ProfilePictureUrl = PUrl,
            IsActivated = true
        };
        parentClinic.TotalBranches++;
        // create account for branch
        var branchAccount = new Staff
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FirstName = parentClinic.Name,
            LastName = request.Name,
            Password = passwordHasherService.HashPassword("123456789"),
            Status = 1,
            PhoneNumber = request.PhoneNumber,
            City = request.City,
            Ward = request.Ward,
            District = request.District,
            Address = request.Address,
            RoleId = role.Id
        };
        userClinicRepository.Add(new UserClinic
        {
            ClinicId = clinic.Id,
            UserId = branchAccount.Id
        });
        clinicRepository.Add(clinic);
        staffRepository.Add(branchAccount);
        return Result.Success();
    }
}