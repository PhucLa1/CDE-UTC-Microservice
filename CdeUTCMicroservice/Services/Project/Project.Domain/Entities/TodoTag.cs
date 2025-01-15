namespace Project.Domain.Entities
{
    public class TodoTag : Entity<TodoTagId>
    {
        public TodoId TodoId { get; private set; } = default!;
        public TagId TagId { get; private set; } = default!;
        public static TodoTag Create(TodoId todoId, TagId tagId)
        {
            var todoTag = new TodoTag
            {
                TodoId = todoId,
                TagId = tagId
            };
            return todoTag;
        }
    }
}
