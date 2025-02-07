namespace Project.Domain.Entities
{
    public class FolderComment : BaseEntity
    {
        public string Content { get; set; } = default!;
        public int? FolderId { get; set; }
        
    }
}
