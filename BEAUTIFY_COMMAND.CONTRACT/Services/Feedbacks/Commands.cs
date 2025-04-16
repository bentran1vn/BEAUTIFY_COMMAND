using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Feedbacks;

public class Commands
{
    public class ScheduleFeedback
    {
        public Guid CustomerScheduleId { get; set; }
        public int Rating { get; set; }
        public string? Content { get; set; }
    };
    public class CreateFeedbackCommand: ICommand
    {
        public Guid OrderId { get; set; }
        public IFormFileCollection Images { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public List<ScheduleFeedback> ScheduleFeedbacks { get; set; } 
    }
    
    public class CreateFeedbackBody: ICommand
    {
        public Guid OrderId { get; set; }
        public IFormFileCollection Images { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public string ScheduleFeedbacks { get; set; } 
    }
    
    public class UpdateFeedbackBody
    {
        public Guid FeedbackId { get; set; }
        public IFormFileCollection? Images { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public string ScheduleFeedbacks { get; set; }  
    }
    
    public class UpdateFeedbackCommand: ICommand
    {
        public Guid FeedbackId { get; set; }
        public IFormFileCollection? Images { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public List<ScheduleFeedback> ScheduleFeedbacks { get; set; } 
    }
    
    public class ViewFeedbackCommand: ICommand
    {
        public Guid FeedbackId { get; set; }
        public bool IsDisplay { get; set; }
    }
    
    
}