using BuildingBlocks.Enums;

namespace BuildingBlocks.Messaging.Events
{
    public class CreateActivityEvent : IntergrationEvent
    {
        public string Action { get; set; } = string.Empty;
        public int ResourceId { get; set; } //Tác nhân bị tác động
        public string Content { get; set; } = default!;
        public TypeActivity TypeActivity { get; set; }
        public int ProjectId { get; set; }
    }
}
