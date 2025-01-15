using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FileComment : Entity<FileCommentId>
    {
        public string Content { get; private set; } = default!;
        public FileId FileId { get; private set; } = default!;

        public static FileComment Create(string content, FileId fileId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(content);
            var fileComment = new FileComment
            {
                Content = content,
                FileId = fileId,
            };
            return fileComment;
        }
    }
}
