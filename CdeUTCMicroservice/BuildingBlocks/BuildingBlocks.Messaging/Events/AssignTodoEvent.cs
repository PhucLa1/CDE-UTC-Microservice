namespace BuildingBlocks.Messaging.Events
{
    public class AssignTodoEvent : IntergrationEvent
    {
        public List<string> UserNames { get; set; }
        public List<string> Emails { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
    }
}
