using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class ViewTag : BaseEntity<ViewTagId>
    {
        public ViewId? ViewId { get; set; } = default!;
        public TagId? TagId { get; set; } = default!;
        
    }
}
