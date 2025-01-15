namespace Project.Domain.Entities
{
    public class TodoComment : Entity<TodoCommentId>
    {
        public string Content { get; private set; } = default!;
        public TodoId TodoId { get; private set; } = default!;
        public static TodoComment Create(string content, TodoId todoId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(content);
            var todoComment = new TodoComment
            {
                Content = content,
                TodoId = todoId
            };
            return todoComment;
        }
    }
}
