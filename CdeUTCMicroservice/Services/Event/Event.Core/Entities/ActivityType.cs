using BuildingBlocks.Enums;
using Event.Core.Entities.Base;

namespace Event.Core.Entities
{
    public class ActivityType : BaseEntity
    {
        public TypeActivity TypeActivity { get; set; }
        public int ProjectId { get; set; }
        public bool IsAcceptAll { get; set; }
        public TimeSpan TimeSend { get; set; }
        public bool Enabled { get; set; }
    }
}
