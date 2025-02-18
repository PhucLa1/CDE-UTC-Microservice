using Event.Core.Entities.Base;
using Event.Core.Enums;

namespace Event.Core.Entities
{
    public class ActivityType : BaseEntity
    {
        public ActivityTypeMode ActivityTypeMode { get; set; } = ActivityTypeMode.Instant;
        public ActivityTypeCategory ActivityTypeCategory { get; set; }
        public string Name { get; set; } = default!;
        public string Template { get; set; } = default!;
        public int? ActivityTypeParentId { get; set; }
        public ActivityTypeParent? ActivityTypeParent { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
