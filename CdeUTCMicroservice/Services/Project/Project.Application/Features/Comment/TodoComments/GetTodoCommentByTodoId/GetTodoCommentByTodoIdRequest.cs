namespace Project.Application.Features.Comment.TodoComments.GetTodoCommentByTodoId
{
    public class GetTodoCommentByTodoIdRequest : IQuery<ApiResponse<List<GetTodoCommentByTodoIdResponse>>>
    {
        public int TodoId { get; set; }
    }
}
