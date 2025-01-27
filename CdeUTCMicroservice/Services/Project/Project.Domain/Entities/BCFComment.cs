namespace Project.Domain.Entities
{
    public class BCFComment : Entity<BCFCommentId>
    {
        public string Content { get; set; } = default!;
        public BCFTopicId? BCFTopicId { get; set; } = default!;
        public BCFTopic? BCFTopic { get; set; }
    }
}
