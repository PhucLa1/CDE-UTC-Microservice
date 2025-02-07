namespace Project.Domain.Entities
{
    public class FolderTag : BaseEntity
    {
        public int? TagId { get; set; }
        public int? FolderId { get; set; }
        
    }
}
