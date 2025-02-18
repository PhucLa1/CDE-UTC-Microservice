namespace Project.Domain.Entities
{
    public class FileHistory : BaseEntity
    {
        public int? FileId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Size { get; set; }
        public string Url { get; set; } = string.Empty;
        public int FileVersion { get; set; }
        public FileType FileType { get; set; }
        public string MimeType { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
    }
}
