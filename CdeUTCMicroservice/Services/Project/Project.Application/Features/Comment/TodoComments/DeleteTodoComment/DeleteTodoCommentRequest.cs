namespace Project.Application.Features.Comment.TodoComments.DeleteTodoComment
{
    public record DeleteTodoCommentRequest : ICommand<DeleteTodoCommentResponse>
    {
        public int Id { get; set; }
    }
} 