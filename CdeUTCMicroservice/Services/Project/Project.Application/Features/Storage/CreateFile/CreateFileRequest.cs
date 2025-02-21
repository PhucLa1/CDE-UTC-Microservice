namespace Project.Application.Features.Storage.CreateFile
{
    public class CreateFileRequest : ICommand<CreateFileResponse>
    {
        public string Name { get; set; } = default!;
        public decimal Size { get; set; }
        public string Url { get; set; } = default!;
        public int? FolderId { get; set; }
        public int? ProjectId { get; set; }
        public string MimeType { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
    }
}
