using Project.Domain.Entities;
using Type = Project.Domain.Entities.Type;

namespace Project.Domain.ValueObjects
{
    public class Characteristic
    {
        public int? TypeId { get; set; }
        public Type? Type { get; set; } // Navigation Property

        public int? StatusId { get; set; }
        public Status? Status { get; set; } // Navigation Property

        public int? PriorityId { get; set; }
        public Priority? Priority { get; set; } // Navigation Property
    }
}
