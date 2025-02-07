
namespace Project.Domain.Entities
{
    public class ViewTodo : BaseEntity
    {
        public int? ViewId { get; set; } = default!;
        public int? TodoId { get; set; } = default!;
        
    }
}
