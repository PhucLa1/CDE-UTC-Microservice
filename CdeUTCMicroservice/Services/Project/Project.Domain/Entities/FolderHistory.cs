namespace Project.Domain.Entities
{
    public class FolderHistory : BaseEntity
    {
        public string Name { get; set; } = default!;
        public int? FolderId { get; set; }
        public int Version { get; set; }
        public Folder? Folder { get; set; }
    }
}
