using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FolderTag : Entity<FolderTagId>
    {
        public TagId TagId { get; private set; } = default!;
        public FolderId FolderId { get; private set; } = default!;
        public static FolderTag Create(TagId tagId, FolderId folderId)
        {
            var folderTag = new FolderTag
            {
                TagId = tagId,
                FolderId = folderId
            };
            return folderTag;
        }
    }
}
