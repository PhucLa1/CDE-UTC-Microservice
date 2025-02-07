
namespace Project.Domain.Entities
{
    public class ViewTag : BaseEntity
    {
        public int? ViewId { get; set; } = default!;
        public int? TagId { get; set; } = default!;
        
    }
}
