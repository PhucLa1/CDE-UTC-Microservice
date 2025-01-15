using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class Folder : Aggregate<FolderId>
    {
        public string Name { get; private set; } = default!;
        public ProjectId ProjectId { get; private set; } = default!;
        public FolderVersion FolderVersion { get; private set; }
        public FolderId ParentId { get; private set; } = default!;
        public bool IsCheckin { get; private set; }
        public bool IsCheckout { get; private set; }
        public static Folder Create(string name, ProjectId projectId, FolderVersion folderVersion, FolderId parentId, bool isCheckin, bool isCheckout)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            var folder = new Folder
            {
                Name = name,
                ProjectId = projectId,
                FolderVersion = folderVersion,
                ParentId = parentId,
                IsCheckin = isCheckin,
                IsCheckout = isCheckout
            };
            return folder;
        }
    }
}
