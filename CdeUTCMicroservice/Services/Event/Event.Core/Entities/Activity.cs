using Event.Core.Entities.Base;

namespace Event.Core.Entities
{
    public class Activity : BaseEntity
    {
        public string Action { get; set; } = default!;
        public string Content { get; set; } = default!;
        public int ProjectId { get; set; }
        public int? ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; } = default!;
    }
}
