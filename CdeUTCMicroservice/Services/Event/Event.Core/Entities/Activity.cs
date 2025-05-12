using BuildingBlocks.Enums;
using Event.Core.Entities.Base;
namespace Event.Core.Entities
{
    public class Activity : BaseEntity
    {
        public string Action { get; set; } = string.Empty;
        public int ResourceId { get; set; } //Tác nhân bị tác động
        public string Content { get; set; } = default!;
        public TypeActivity TypeActivity { get; set; }
        public int ProjectId { get; set; }
    }
}
