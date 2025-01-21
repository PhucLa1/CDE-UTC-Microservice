namespace Event.Shared.DTOs.Models
{
    public class RecipentEmail
    {
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
