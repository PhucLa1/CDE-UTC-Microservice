namespace Project.Domain.Entities
{
    public class FolderPermission : BaseEntity
    {
        public int? FolderId { get; set; }
        public int UserId { get; set; } = default!;
        public Access Access { get; set; } = default!;
        public bool IsApplyAll  { get; set; } = true;
        
    }
}
