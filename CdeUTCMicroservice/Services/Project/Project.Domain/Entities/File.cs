namespace Project.Domain.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; } = default!;
        public decimal Size { get; set; }
        public string Url { get; set; } = default!;
        public int? FolderId { get; set; } 
        public int? ProjectId { get; set; }
        public int FileVersion { get; set; }
        public bool IsCheckIn { get; set; }
        public bool IsCheckout { get; set; } = true;
        public FileType FileType { get; set; } = default!;
        public string MimeType { get; set; } = default!;
        public string Extension { get; set; } = default!;
        
    }

}
