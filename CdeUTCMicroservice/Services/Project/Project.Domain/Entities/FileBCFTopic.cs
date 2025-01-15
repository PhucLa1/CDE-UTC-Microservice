using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FileBCFTopic : Entity<FileBCFTopicId>
    {
        public FileId FileId { get; private set; } = default!;
        public BCFTopicId BCFTopicId { get; private set; } = default!;

        public static FileBCFTopic Create(FileId fileId, BCFTopicId bcfTopicId)
        {
            return new FileBCFTopic
            {
                FileId = fileId,
                BCFTopicId = bcfTopicId,
            };
        }
    }
}
