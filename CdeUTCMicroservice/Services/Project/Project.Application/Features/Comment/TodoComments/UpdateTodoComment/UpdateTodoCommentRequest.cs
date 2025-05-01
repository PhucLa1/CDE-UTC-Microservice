namespace Project.Application.Features.Comment.TodoComments.UpdateTodoComment
{
    public record UpdateTodoCommentRequest : ICommand<UpdateTodoCommentResponse>
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
    }
} 