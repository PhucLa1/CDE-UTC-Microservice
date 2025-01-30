using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FolderComment : BaseEntity<FolderCommentId>
    {
        public string Content { get; set; } = default!;
        public FolderId? FolderId { get; set; }
        
    }
}
