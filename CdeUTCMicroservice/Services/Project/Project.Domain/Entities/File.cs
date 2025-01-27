using Project.Domain.Extensions;
using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class File : Aggregate<FileId>
    {
        public string Name { get; set; } = default!;
        public decimal Size { get; set; }
        public string Url { get; set; } = default!;
        public FolderId? FolderId { get; set; } 
        public ProjectId? ProjectId { get; set; }
        public FileVersion FileVersion { get; set; }
        public bool IsCheckIn { get; set; }
        public bool IsCheckout { get; set; } = true;
        public FileType FileType { get; set; } = default!;
        public string MimeType { get; set; } = default!;
        public string Extension { get; set; } = default!;
        
    }

}
