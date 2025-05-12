namespace Project.Application.Features.Comment.TodoComments.GetTodoCommentByTodoId
{
    public class GetTodoCommentByTodoIdResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }
    }
}
