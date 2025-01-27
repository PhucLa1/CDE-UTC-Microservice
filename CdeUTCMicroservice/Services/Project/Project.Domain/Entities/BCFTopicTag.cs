namespace Project.Domain.Entities
{
    public class BCFTopicTag : Entity<BCFTopicTagId>
    {
        public BCFTopicId? BCFTopicId { get; set; } = default!;
        public TagId? TagId { get; set; } = default!;

        public BCFTopic? BCFTopic { get; set; }
        public Tag? Tag { get; set; }

    }
}
