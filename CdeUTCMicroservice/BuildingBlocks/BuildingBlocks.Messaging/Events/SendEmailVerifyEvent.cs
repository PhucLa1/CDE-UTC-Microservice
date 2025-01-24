namespace BuildingBlocks.Messaging.Events
{
    public class SendEmailVerifyEvent : IntergrationEvent
    {
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiredTime { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
