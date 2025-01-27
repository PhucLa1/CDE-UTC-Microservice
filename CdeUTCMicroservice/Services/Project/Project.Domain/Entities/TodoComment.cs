namespace Project.Domain.Entities
{
    public class TodoComment : Entity<TodoCommentId>
    {
        public string Content { get; set; } = default!;
        public TodoId? TodoId { get; set; }

    }
}
