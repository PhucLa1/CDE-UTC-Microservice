using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FileTag : BaseEntity<FileTagId>
    {
        public FileId? FileId { get; set; } = default!;
        public TagId? TagId { get; set; } = default!;

    }
}
