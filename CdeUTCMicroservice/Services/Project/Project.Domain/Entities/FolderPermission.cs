namespace Project.Domain.Entities
{
    public class FolderPermission : BaseEntity
    {
        public int? FolderId { get; set; }
        public int TargetId { get; set; } = default!;
        public bool IsGroup { get; set; }
        public Access Access { get; set; } = default!;
        public bool IsApplyAll  { get; set; } = true;
        public Folder? Folder { get; set; }

    }
}
