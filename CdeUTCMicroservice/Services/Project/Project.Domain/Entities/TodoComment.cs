namespace Project.Domain.Entities
{
    public class TodoComment : BaseEntity<TodoCommentId>
    {
        public string Content { get; set; } = default!;
        public TodoId? TodoId { get; set; }

    }
}
