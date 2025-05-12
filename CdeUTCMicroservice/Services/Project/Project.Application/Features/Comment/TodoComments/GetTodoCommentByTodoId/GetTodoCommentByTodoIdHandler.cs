using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Comment.TodoComments.GetTodoCommentByTodoId
{
    public class GetTodoCommentByTodoIdHandler
        (IBaseRepository<TodoComment> todoCommentRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetTodoCommentByTodoIdRequest, ApiResponse<List<GetTodoCommentByTodoIdResponse>>>
    {
        public async Task<ApiResponse<List<GetTodoCommentByTodoIdResponse>>> Handle(GetTodoCommentByTodoIdRequest request, CancellationToken cancellationToken)
        {
            var currentDateDisplay = todoCommentRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = todoCommentRepository.GetCurrentTimeDisplay();
            var todoComments = await todoCommentRepository.GetAllQueryAble()
                .Where(e => e.TodoId == request.TodoId).ToListAsync();

            var updateById = todoComments.Select(e => e.UpdatedBy).Distinct().ToList();

            var users = await userGrpc
                .GetUsersByIds(new GetUserRequestGrpc { Ids = updateById });

            var result = (from tc in todoComments
                          join u in users on tc.UpdatedBy equals u.Id
                          select new GetTodoCommentByTodoIdResponse()
                          {
                              Id = tc.Id,
                              Email = u.Email,
                              Name = u.FullName,
                              AvatarUrl = u.ImageUrl,
                              Content = tc.Content,
                              UpdatedAt = tc.UpdatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                              UpdatedBy = tc.UpdatedBy,
                          }).ToList();

            return new ApiResponse<List<GetTodoCommentByTodoIdResponse>> { Data = result, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
