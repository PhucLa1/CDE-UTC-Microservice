using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FileTodo : Entity<FileTodoId>
    {
        public FileId FileId { get; private set; } = default!;
        public TodoId TodoId { get; private set; } = default!;

        public static FileTodo Create(FileId fileId, TodoId todoId)
        {
            var fileTodo = new FileTodo
            {
                FileId = fileId,
                TodoId = todoId
            };
            return fileTodo;
        }
    }
}
