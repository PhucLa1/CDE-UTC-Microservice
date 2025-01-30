using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FileTodo : BaseEntity<FileTodoId>
    {
        public FileId? FileId { get; set; } = default!;
        public TodoId? TodoId { get; set; } = default!;


    }
}
