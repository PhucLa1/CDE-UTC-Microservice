namespace BuildingBlocks.Messaging.Events
{
    public class InvitationUserEvent : IntergrationEvent
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public int UserId { get; set; }

    }
}
