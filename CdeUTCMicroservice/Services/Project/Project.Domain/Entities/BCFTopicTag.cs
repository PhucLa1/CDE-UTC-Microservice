namespace Project.Domain.Entities
{
    public class BCFTopicTag : Entity<BCFTopicTagId>
    {
        public BCFTopicId BCFTopicId { get; private set; } = default!;
        public TagId TagId { get; private set; } = default!;
        public static BCFTopicTag Create(BCFTopicId bcfTopicId, TagId tagId)
        {
            return new BCFTopicTag
            {
                TagId = tagId,
                BCFTopicId = bcfTopicId,
            };
        }
    }
}
