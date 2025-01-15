using Event.Core.Entities.Base;

namespace Event.Core.Entities
{
    public class ActivityTypeParent : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string TimeSend { get; set; } = default!;
        public string IconImageUrl { get; set; } = default!;
        public ICollection<ActivityType> ActivityTypes { get; set; } = new List<ActivityType>();
    }
}
