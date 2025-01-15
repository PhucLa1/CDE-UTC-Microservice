using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FolderComment : Entity<FolderCommentId>
    {
        public string Content { get; private set; } = default!;
        public FolderId FolderId { get; private set; } = default!;
        public static FolderComment Create(string content, FolderId folderId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(content);
            var folderComment = new FolderComment
            {
                Content = content,
                FolderId = folderId
            };
            return folderComment;
        }
    }
}
