using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FolderPermission : Entity<FolderPermissionId>
    {
        public FolderId FolderId { get; private set; } = default!;
        public Guid UserId { get; private set; } = default!;
        public Access Access { get; private set; } = default!;
        public bool IsApplyAll  { get; private set; } = true;
        public static FolderPermission Create(FolderId folderId, Guid userId, Access access)
        {
            var folderPermission = new FolderPermission
            {
                FolderId = folderId,
                UserId = userId,
                Access = access
            };
            return folderPermission;
        }
    }
}
