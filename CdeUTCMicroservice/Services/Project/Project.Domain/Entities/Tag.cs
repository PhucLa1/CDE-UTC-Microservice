namespace Project.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public int? ProjectId { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<BCFTopicTag>? BCFTopicTags { get; set; }
        public ICollection<FolderTag>? FolderTags { get; set; }
    }
}
