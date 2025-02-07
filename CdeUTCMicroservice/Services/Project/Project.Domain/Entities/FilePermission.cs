namespace Project.Domain.Entities
{
    public class FilePermission : BaseEntity
    {
        public int? FileId { get; set; }
        public int UserId { get; set; }
        public Access Access { get; set; } = Access.Write;

    }
}
