namespace Project.Domain.Entities
{
    public class BCFComment : BaseEntity
    {
        public string Content { get; set; } = default!;
        public int? BCFTopicId { get; set; } = default!;
        public BCFTopic? BCFTopic { get; set; }
    }
}
