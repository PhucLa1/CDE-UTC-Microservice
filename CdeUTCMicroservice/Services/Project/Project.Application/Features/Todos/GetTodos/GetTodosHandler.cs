

using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Todos.GetTodos
{
    public class GetTodosHandler
        (IBaseRepository<Todo> todoRepository,
        IBaseRepository<Priority> priorityRepository,
        IBaseRepository<Status> statusRepository,
        IBaseRepository<Type> typeRepository,
        IBaseRepository<Tag> tagRepository,
        IBaseRepository<TodoTag> todoTagRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetTodosRequest, ApiResponse<List<GetTodosResponse>>>
    {
        public async Task<ApiResponse<List<GetTodosResponse>>> Handle(GetTodosRequest request, CancellationToken cancellationToken)
        {
            var currentDateDisplay = todoRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = todoRepository.GetCurrentTimeDisplay();

            var todo = await todoRepository.GetAllQueryAble().Where(e => e.ProjectId == request.ProjectId)
                .Select(e => new GetTodosResponse()
                {
                    Id = e.Id,
                    Name = e.Name,
                    IsAssignToGroup = e.IsAssignToGroup,
                    AssignTo = e.AssignTo,
                    CreatedBy = e.CreatedBy,
                    Priority = priorityRepository.GetAllQueryAble().First(p => p.Id == e.PriorityId),
                    Status = statusRepository.GetAllQueryAble().First(s => s.Id == e.StatusId),
                    Type = typeRepository.GetAllQueryAble().First(t => t.Id == e.TypeId),
                    Description = e.Description,
                    StartDate = e.StartDate.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    DueDate = e.DueDate.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    Tags = (from tt in todoTagRepository.GetAllQueryAble() 
                            join t in tagRepository.GetAllQueryAble() on tt.TagId equals t.Id
                            where tt.TodoId == e.Id
                            select t).ToList(),
                }).ToListAsync(cancellationToken);

            var idCreated = todo.Select(e => e.CreatedBy).Distinct().ToList();
            var users = await userGrpc
                .GetUsersByIds(new GetUserRequestGrpc { Ids = idCreated });

            var todos = (from t in todo
                         join u in users on t.CreatedBy equals u.Id
                         select new GetTodosResponse()
                         {
                             Id = t.Id,
                             Name = t.Name,
                             IsAssignToGroup = t.IsAssignToGroup,
                             AssignTo = t.AssignTo,
                             CreatedBy = t.CreatedBy,
                             Priority = t.Priority,
                             Status = t.Status,
                             Type = t.Type,
                             Description = t.Description,
                             StartDate = t.StartDate,
                             DueDate = t.DueDate,
                             NameCreatedBy = u.FullName,
                             Tags = t.Tags,
                         }).ToList();


            return new ApiResponse<List<GetTodosResponse>> { Data = todos, Message = Message.GET_SUCCESSFULLY };

        }
    }
}
