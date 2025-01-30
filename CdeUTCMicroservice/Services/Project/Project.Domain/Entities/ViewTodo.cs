using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class ViewTodo : BaseEntity<ViewTodoId>
    {
        public ViewId? ViewId { get; set; } = default!;
        public TodoId? TodoId { get; set; } = default!;
        
    }
}
