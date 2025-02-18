namespace Project.Domain.Entities
{
    public class FolderTag : BaseEntity
    {
        public int? TagId { get; set; }
        public int? FolderId { get; set; }
        public Tag? Tag { get; set; }
        public Folder? Folder { get; set; }

    }
}
