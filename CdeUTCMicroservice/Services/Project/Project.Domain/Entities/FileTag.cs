using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FileTag : Entity<FileTagId>
    {
        public FileId FileId { get; private set; } = default!;
        public TagId TagId { get; private set; } = default!;

        public static FileTag Create(FileId fileId, TagId tagId)
        {
            var fileTag = new FileTag
            {
                FileId = fileId,
                TagId = tagId
            };
            return fileTag;
        }
    }
}
