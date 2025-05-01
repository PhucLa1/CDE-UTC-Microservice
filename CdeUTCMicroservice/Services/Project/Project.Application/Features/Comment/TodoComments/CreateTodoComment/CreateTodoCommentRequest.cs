namespace Project.Application.Features.Comment.TodoComments.CreateTodoComment
{
    public record CreateTodoCommentRequest : ICommand<CreateTodoCommentResponse>
    {
        public int TodoId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
} 