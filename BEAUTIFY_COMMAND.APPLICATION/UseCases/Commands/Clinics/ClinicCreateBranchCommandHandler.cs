namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class
    ClinicCreateBranchCommandHandler(
        IRepositoryBase<Staff, Guid> staffRepository,
        IRepositoryBase<Clinic, Guid> clinicRepository,
        IMediaService mediaService,
        IPasswordHasherService passwordHasherService,
        IRepositoryBase<Role, Guid> roleRepository,
        ICurrentUserService currentUserService,
        IRepositoryBase<UserClinic, Guid> userClinicRepository,
        IRepositoryBase<SystemTransaction, Guid> systemTransactionRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicCreateBranchCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicCreateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var parentClinic = await clinicRepository.FindByIdAsync(currentUserService.ClinicId.Value, cancellationToken) ??
                           throw new ClinicException.ClinicNotFoundException(currentUserService.ClinicId.Value);
        
        if (parentClinic.TotalBranches >= parentClinic.AdditionBranches)
            return Result.Failure(new Error("403", "You have reached the maximum number of branches"));
        
        var isExist = await clinicRepository
            .FindAll(x => x.PhoneNumber == request.PhoneNumber && x.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);
        
        var role = await roleRepository.FindSingleAsync(x => x.Name == "Clinic Staff", cancellationToken);
        var oUrl = await mediaService.UploadImageAsync(request.OperatingLicense);
        
        if(role == null)
            return Result.Failure(new Error("404", "Role not found"));

        if (isExist != null)
            return Result.Failure(new Error("400", "Clinic with phone number already exists"));

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
            WorkingTimeStart = request.WorkingTimeStart,
            WorkingTimeEnd = request.WorkingTimeEnd,
            PhoneNumber = request.PhoneNumber,
            TaxCode = parentClinic.TaxCode,
            BusinessLicenseUrl = parentClinic.BusinessLicenseUrl,
            OperatingLicenseUrl = oUrl,
            OperatingLicenseExpiryDate = request.OperatingLicenseExpiryDate,
            Status = 1,
            BankName = request.BankName,
            BankAccountNumber = request.BankAccountNumber,
            IsActivated = true
        };
        
        if (request.ProfilePictureUrl != null)
        {
            var pUrl = await mediaService.UploadImageAsync(request.ProfilePictureUrl);
            clinic.ProfilePictureUrl = pUrl;
        }

        parentClinic.TotalBranches++;

        // var branchAccount = new Staff
        // {
        //     Id = Guid.NewGuid(),
        //     Email = request.Email,
        //     FirstName = request.Name,
        //     LastName = $" ({parentClinic.Name})",
        //     Password = passwordHasherService.HashPassword("123456789"),
        //     Status = 1,
        //     PhoneNumber = request.PhoneNumber,
        //     City = request.City,
        //     Ward = request.Ward,
        //     District = request.District,
        //     Address = request.Address,
        //     RoleId = role.Id
        // };
        //
        // userClinicRepository.Add(new UserClinic
        // {
        //     ClinicId = clinic.Id,
        //     UserId = branchAccount.Id
        // });
        //
        // clinicRepository.Add(clinic);
        //
        // staffRepository.Add(branchAccount);
        
        return Result.Success();
    }
}