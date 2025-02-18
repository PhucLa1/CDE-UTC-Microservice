namespace Project.Application.Features.Storage.CreateFile
{
    public class CreateFileRequest
    {
        public string Name { get; set; } = default!;
        public decimal Size { get; set; }
        public string Url { get; set; } = default!;
        public int? FolderId { get; set; }
        public int? ProjectId { get; set; }
        public FileType FileType { get; set; } = default!;
        public string MimeType { get; set; } = default!;
        public string Extension { get; set; } = default!;
    }
}
