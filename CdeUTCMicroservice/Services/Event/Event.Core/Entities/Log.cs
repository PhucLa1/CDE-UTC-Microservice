using Event.Core.Entities.Base;

namespace Event.Core.Entities
{
    public class Log : BaseEntity
    {
        public int StatusCode { get; set; }
        public string Method { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string? Data { get; set; }
    }
}
