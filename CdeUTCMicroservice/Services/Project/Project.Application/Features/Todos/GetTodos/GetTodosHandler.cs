

namespace Project.Application.Features.Todos.GetTodos
{
    public class GetTodosHandler : IQueryHandler<GetTodosRequest, ApiResponse<List<GetTodosResponse>>>
    {
        public Task<ApiResponse<List<GetTodosResponse>>> Handle(GetTodosRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
