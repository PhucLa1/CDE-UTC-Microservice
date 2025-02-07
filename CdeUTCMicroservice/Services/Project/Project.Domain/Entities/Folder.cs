namespace Project.Domain.Entities
{
    public class Folder : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int? ProjectId { get; set; }
        public FolderVersion FolderVersion { get; set; }
        public int ParentId { get; set; } = default!;
        public bool IsCheckin { get; set; }
        public bool IsCheckout { get; set; }
        
    }
}
