using Project.Domain.Extensions;
using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class File : Aggregate<FileId>
    {
        public string Name { get; private set; } = default!;
        public decimal Size { get; private set; }
        public string Url { get; private set; } = default!;
        public FolderId FolderId { get; private set; } = default!;
        public ProjectId ProjectId { get; private set; } = default!;
        public FileVersion FileVersion { get; private set; }
        public bool IsCheckIn { get; private set; }
        public bool IsCheckout { get; private set; } = true;
        public FileType FileType { get; private set; } = default!;
        public string MimeType { get; private set; } = default!;
        public string Extension { get; private set; } = default!;
        public static File Create(FileId id, string name, decimal size, string url, FolderId folderId, ProjectId projectId, FileVersion fileVersion, bool isCheckIn, bool isCheckout, string mimeType, string extension)
        {
            if (name == null) throw new ArgumentNullException("Name cannot be null!");
            var file =  new File()
            {
                Id = id,
                Name = name,
                Size = size,
                Url = url,
                FolderId = folderId,
                ProjectId = projectId,
                FileVersion = fileVersion,
                IsCheckIn = isCheckIn,
                IsCheckout = isCheckout,
                FileType = IconFileExtension.GetFileType(name),
                MimeType = mimeType,
                Extension = extension,
            };
            return file;
        }
    }

}
