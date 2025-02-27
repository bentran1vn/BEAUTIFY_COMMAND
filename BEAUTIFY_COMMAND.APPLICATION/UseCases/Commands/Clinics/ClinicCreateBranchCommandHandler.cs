using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class
    ClinicCreateBranchCommandHandler(
        IRepositoryBase<User, Guid> userRepository,
        IRepositoryBase<Clinic, Guid> clinicRepository,
        IMediaService mediaService,
        IPasswordHasherService passwordHasherService,
        IRepositoryBase<Role, Guid> roleRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicCreateBranchCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicCreateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var parentClinic = await clinicRepository.FindByIdAsync(request.ParentId, cancellationToken) ??
                           throw new ClinicException.ClinicNotFoundException(request.ParentId);
        var role = await roleRepository.FindSingleAsync(x => x.Name == "Clinic Staff", cancellationToken);
        var OUrl = await mediaService.UploadImageAsync(request.OperatingLicense);
        var PUrl = await mediaService.UploadImageAsync(request.ProfilePictureUrl);
        var clinic = new Clinic
        {
            Email = request.Email,
            Name = request.Name,
            Address = request.Address,
            ParentId = request.ParentId,
            PhoneNumber = request.PhoneNumber,
            TaxCode = parentClinic.TaxCode,
            BusinessLicenseUrl = parentClinic.BusinessLicenseUrl,
            OperatingLicenseUrl = OUrl,
            OperatingLicenseExpiryDate = request.OperatingLicenseExpiryDate,
            Status = 1,
            ProfilePictureUrl = PUrl,
            IsActivated = true,
        };
        parentClinic.TotalBranches++;
        // create account for branch
        var branchAccount = new User
        {
            Email = request.Email,
            FirstName = parentClinic.Name,
            LastName = request.Name,
            Password = passwordHasherService.HashPassword("123456789"),
            Status = 1,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            RoleId = role.Id,
        };

        clinicRepository.Add(clinic);
        userRepository.Add(branchAccount);
        return Result.Success();
    }
}