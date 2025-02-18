using Event.Core.Entities.Base;

namespace Event.Core.Entities
{
    public class Activity : BaseEntity
    {
        public string Action { get; set; } = string.Empty;
        public int ResourceId { get; set; } //Tác nhân bị tác động
        public string Content { get; set; } = default!;
        public int? ActivityTypeId { get; set; }
        public ActivityType? ActivityType { get; set; }
    }
}
