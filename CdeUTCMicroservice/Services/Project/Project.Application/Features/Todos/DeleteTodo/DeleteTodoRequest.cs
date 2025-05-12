namespace Project.Application.Features.Todos.DeleteTodo
{
    public class DeleteTodoRequest : ICommand<DeleteTodoResponse>
    {
        public int Id { get; set; }
    }
}
