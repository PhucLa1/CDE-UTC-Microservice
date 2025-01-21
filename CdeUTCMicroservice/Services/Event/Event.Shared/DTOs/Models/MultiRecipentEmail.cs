namespace Event.Shared.DTOs.Models
{
    public class MultiRecipientEmail
    {
        public required List<string> To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
