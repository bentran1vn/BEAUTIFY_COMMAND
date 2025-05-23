using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Events;

public class Commands
{
    public class EventBody
    {
        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }

    public class CreateEventCommand : EventBody, ICommand
    {
        public Guid ClinicId { get; set; }
        public new IFormFile Image { get; set; }
        public CreateEventCommand(EventBody body, Guid clinicId)
        {
            ClinicId = clinicId;
            Image = body.Image ?? throw new Exception("Image is required");
            Name = body.Name;
            Description = body.Description;
            StartDate = body.StartDate;
            EndDate = body.EndDate;
        }
    }
    
    public class UpdateEventCommand : EventBody, ICommand
    {
        public Guid ClinicId { get; set; }
        public Guid Id { get; set; }
        public UpdateEventCommand(EventBody body, Guid id, Guid clinicId)
        {
            ClinicId = clinicId;
            Id = id;
            Image = body.Image;
            Name = body.Name;
            Description = body.Description;
            StartDate = body.StartDate;
            EndDate = body.EndDate;
        }
    }
    
    public class DeleteEventCommand : ICommand
    {
        public DeleteEventCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}