using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FolderPermission : Entity<FolderPermissionId>
    {
        public FolderId? FolderId { get; set; }
        public Guid UserId { get; set; } = default!;
        public Access Access { get; set; } = default!;
        public bool IsApplyAll  { get; set; } = true;
        
    }
}
