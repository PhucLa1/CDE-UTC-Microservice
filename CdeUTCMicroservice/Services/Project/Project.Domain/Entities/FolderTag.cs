using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FolderTag : BaseEntity<FolderTagId>
    {
        public TagId? TagId { get; set; }
        public FolderId? FolderId { get; set; }
        
    }
}
