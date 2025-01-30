using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class View : BaseEntity<ViewId>
    {
        public FileId? FileId { get; set; } = default!;
        public ViewType ViewType { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        
    }
}
