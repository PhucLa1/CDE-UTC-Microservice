namespace Project.Domain.Entities
{
    public class FilePermission : BaseEntity
    {
        public int? FileId { get; set; }
        public int TargetId { get; set; }
        public bool IsGroup { get; set; }
        public Access Access { get; set; } = Access.Write;
        public File? File { get; set; }

    }
}
