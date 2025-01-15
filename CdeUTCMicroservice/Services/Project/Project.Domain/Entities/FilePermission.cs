using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FilePermission : Entity<FilePermissionId>
    {
        public FileId FileId { get; private set; } = default!;
        public Guid UserId { get; private set; }
        public Access Access { get; private set; } = Access.Write;

        public static FilePermission Create(FileId fileId, Guid userId, Access access)
        {
            var filePermission = new FilePermission
            {
                FileId = fileId,
                UserId = userId,
                Access = access
            };
            return filePermission;
        }
    }
}
