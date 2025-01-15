using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class ViewTodo : Entity<ViewTodoId>
    {
        public ViewId ViewId { get; private set; } = default!;
        public TodoId TodoId { get; private set; } = default!;
        public static ViewTodo Create(ViewId viewId, TodoId todoId)
        {
            var viewTodo = new ViewTodo
            {
                ViewId = viewId,
                TodoId = todoId
            };
            return viewTodo;
        }
    }
}
