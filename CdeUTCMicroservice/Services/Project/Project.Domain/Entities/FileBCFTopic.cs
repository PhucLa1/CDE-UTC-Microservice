using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class FileBCFTopic : Entity<FileBCFTopicId>
    {
        public FileId? FileId { get; set; }
        public BCFTopicId? BCFTopicId { get; set; }

        
    }
}
