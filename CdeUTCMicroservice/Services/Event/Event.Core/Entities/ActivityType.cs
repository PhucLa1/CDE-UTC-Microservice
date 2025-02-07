using Event.Core.Entities.Base;

namespace Event.Core.Entities
{
    public class ActivityType : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Template { get; set; } = default!;
        public int? ActivityTypeParentId { get; set; }
        public ActivityTypeParent ActivityTypeParent { get; set; } = default!;
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
