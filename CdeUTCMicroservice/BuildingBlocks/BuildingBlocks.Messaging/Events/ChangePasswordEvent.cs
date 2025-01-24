namespace BuildingBlocks.Messaging.Events
{
    public class ChangePasswordEvent : IntergrationEvent
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
