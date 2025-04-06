using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.ServicePromotions;
public static class Commands
{
    public class CreatePromotionServicesBody
    {
        public Guid ServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double DiscountPercent { get; set; } // Ensure valid default value
        public IFormFile? Image { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDate { get; set; }
    }

    public record CreatePromotionServicesCommand(
        Guid UserId,
        Guid ClinicId,
        Guid ServiceId,
        string Name,
        double DiscountPercent,
        IFormFile? Image,
        DateTime StartDay,
        DateTime EndDate) : ICommand;

    public class UpdatePromotionServicesBody
    {
        public Guid PromotionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double DiscountPercent { get; set; } // Ensure valid default value
        public IFormFile? Image { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActivated { get; set; }
    };

    public record UpdatePromotionServicesCommand(
        Guid ClinicId,
        Guid PromotionId,
        string Name,
        double DiscountPercent,
        IFormFile? Image,
        DateTime StartDay,
        DateTime EndDate,
        bool IsActivated) : ICommand;

    public record DeletePromotionServicesBody(
        Guid PromotionId
    );

    public record DeletePromotionServicesCommand(
        Guid ClinicId,
        Guid PromotionId
    ) : ICommand;
}