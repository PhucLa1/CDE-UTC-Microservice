using Event.Core.Entities.Base;

namespace Event.Core.Entities
{
    public class Activity : BaseEntity
    {
        public string Action { get; set; } = default!;
        public string Content { get; set; } = default!;
        public Guid ProjectId { get; set; }
        public Guid? ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; } = default!;
    }
}
