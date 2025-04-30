namespace Project.Application.Features.Todos.GetTodos
{
    public class GetTodosRequest : IQuery<ApiResponse<List<GetTodosResponse>>>
    {
        public int ProjectId { get; set; }
    }
}
