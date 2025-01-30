using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class Folder : BaseEntity<FolderId>
    {
        public string Name { get; set; } = default!;
        public ProjectId? ProjectId { get; set; }
        public FolderVersion FolderVersion { get; set; }
        public FolderId ParentId { get; set; } = default!;
        public bool IsCheckin { get; set; }
        public bool IsCheckout { get; set; }
        
    }
}
