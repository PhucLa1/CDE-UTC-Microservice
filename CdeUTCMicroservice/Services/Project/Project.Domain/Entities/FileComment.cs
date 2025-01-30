using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FileComment : BaseEntity<FileCommentId>
    {
        public string Content { get; set; } = default!;
        public FileId? FileId { get; set; }

       
    }
}
