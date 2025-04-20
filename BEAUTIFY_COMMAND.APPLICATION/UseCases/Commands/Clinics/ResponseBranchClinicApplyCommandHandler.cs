using BEAUTIFY_COMMAND.CONTRACT.MailTemplates;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;

public class ResponseBranchClinicApplyCommandHandler(
    IRepositoryBase<ClinicOnBoardingRequest, Guid> clinicOnBoardingRequestRepository,
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IMailService mailService
    ) : ICommandHandler<CONTRACT.Services.Clinics.Commands.ResponseClinicBranchApplyCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ResponseClinicBranchApplyCommand request, CancellationToken cancellationToken)
    {
        var applyRequest =
            await clinicOnBoardingRequestRepository.FindByIdAsync(new Guid(request.RequestId), cancellationToken,
                x => x.Clinic!);

        if (applyRequest == null) return Result.Failure(new Error("404", "Clinic Request Not Found"));

        if (applyRequest!.Status != 0 || applyRequest.IsDeleted)
            return Result.Failure(new Error("400", "Clinic Apply Request is Handled"));

        if (request.Action != 0 && request.RejectReason == null)
            return Result.Failure(new Error("400", "Missing Reject reason for Rejected or Banned Response"));

        var clinicMain = await clinicRepository.FindAll(x => x.Id.Equals(applyRequest.Clinic!.ParentId)).FirstOrDefaultAsync(cancellationToken);
        
        if (clinicMain == null)
            return Result.Failure(new Error("404", "Clinic Main Not Found"));

        var content = new MailContent
        {
            To = applyRequest.Clinic!.Email,
            Subject = "Clinic Application Decision"
        };
        
        applyRequest.Status = request.Action + 1;

        if (request.Action != 0)
        {
            // 1 is Rejected
            applyRequest.RejectReason = request.RejectReason;
            applyRequest.Clinic!.Status = request.Action + 1;
            
            content.Body = ClinicApplicationEmailTemplates.GetRejectedTemplate(applyRequest.Clinic.Email,
                applyRequest.RejectReason);
        }
        else
        {
            // 0 is Approved
            applyRequest.Clinic.Status = 1;
            applyRequest.Clinic.IsActivated = true;
            
            // Cong vao clinic
            clinicMain.AdditionBranches -= 1;
            clinicMain.TotalBranches += 1;
            
            clinicRepository.Update(clinicMain);
            
            content.Body =
                ClinicApplicationEmailTemplates.GetBranchApprovedTemplate(applyRequest.Clinic.Email);
        }
        
        await mailService.SendMail(content);
        
        return Result.Success("Response the request successfully !");
    }
}