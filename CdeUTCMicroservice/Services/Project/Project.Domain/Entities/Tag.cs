namespace Project.Domain.Entities
{
    public class Tag : BaseEntity<TagId>
    {
        public ProjectId? ProjectId { get; set; }
        public string Name { get; set; } = default!;
        public string DataNearest { get; set; } = default!;
        public ICollection<BCFTopicTag>? BCFTopicTags { get; set; }
    }
}
