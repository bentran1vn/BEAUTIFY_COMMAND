using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ClinicServices;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class Service : AggregateRoot<Guid>, IAuditableEntity
{
    [MaxLength(50)] public required string Name { get; set; }
    [MaxLength(200)] public required string Description { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }
    public int NumberOfCustomersUsed { get; set; } = 0;
    
    public Guid ClinicId { get; set; }
    public virtual Clinic Clinics { get; set; }
    
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    
    [Column(TypeName = "decimal(18,2)")] public decimal? DiscountPrice { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    public virtual ICollection<ServiceMedia>? ServiceMedias { get; set; }
    public virtual ICollection<Promotion>? Promotions { get; set; }
    public virtual ICollection<Procedure>? Procedures { get; set; }
    public virtual ICollection<CustomerSchedule>? CustomerSchedules { get; set; }
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    public virtual ICollection<ClinicVoucher>? ClinicVouchers { get; set; }

    public static Service RaiseCreateClinicServiceEvent(string Name, string Description,
        string[] CoverImage, string[] DescriptionImage,
        decimal Price, Guid CateId, string CateName,
        string CateDescription, Guid ClinicId, string ClinicName,
        string ClinicEmail, string ClinicAddress, string ClinicPhoneNumber,
        string? ClinicProfilePictureUrl
    )
    {
        var clinicService = new Service()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Description = Description,
            Price = Price,
            CategoryId = CateId,
            ClinicId = ClinicId,
        };
        
        clinicService.RaiseDomainEvent(new DomainEvents.ClinicServiceCreated(
            Guid.NewGuid(), 
            new ClinicServiceEvent.CreateClinicService(
                clinicService.Id, Name, Description, CoverImage, DescriptionImage, Price, 
                new ClinicServiceEvent.Category(CateId, CateName, CateDescription),
                new ClinicServiceEvent.Clinic(ClinicId, ClinicName, ClinicEmail,
                ClinicAddress, ClinicPhoneNumber, ClinicProfilePictureUrl)
            )));

        return clinicService;
    }
}