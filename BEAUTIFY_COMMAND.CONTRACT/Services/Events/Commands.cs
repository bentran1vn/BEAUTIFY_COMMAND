using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Events;

public class Commands
{
    public class EventBody
    {
        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeOnly StartDate { get; set; }
        public TimeOnly EndDate { get; set; }
        public DateOnly Date { get; set; }
    }

    public class CreateEvent : EventBody, ICommand
    {
        public Guid ClinicId { get; set; }
        public new IFormFile Image { get; set; }
        public CreateEvent(EventBody body, Guid clinicId)
        {
            ClinicId = clinicId;
            Image = body.Image ?? throw new Exception("Image is required");
            Name = body.Name;
            Description = body.Description;
            StartDate = body.StartDate;
            EndDate = body.EndDate;
            Date = body.Date;
        }
    }
    
    public class UpdateEvent : EventBody, ICommand
    {
        public Guid ClinicId { get; set; }
        public Guid Id { get; set; }
        public UpdateEvent(EventBody body, Guid id, Guid clinicId)
        {
            ClinicId = clinicId;
            Id = id;
            Image = body.Image;
            Name = body.Name;
            Description = body.Description;
            StartDate = body.StartDate;
            EndDate = body.EndDate;
            Date = body.Date;
        }
    }
    
    public class DeleteEvent : ICommand
    {
        public DeleteEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}