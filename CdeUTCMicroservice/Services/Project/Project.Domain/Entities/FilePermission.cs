using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FilePermission : Entity<FilePermissionId>
    {
        public FileId? FileId { get; set; }
        public Guid UserId { get; set; }
        public Access Access { get; set; } = Access.Write;

    }
}
