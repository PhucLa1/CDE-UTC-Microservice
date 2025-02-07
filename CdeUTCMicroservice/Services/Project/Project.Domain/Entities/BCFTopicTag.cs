namespace Project.Domain.Entities
{
    public class BCFTopicTag : BaseEntity
    {
        public int? BCFTopicId { get; set; }
        public int? TagId { get; set; }

        public BCFTopic? BCFTopic { get; set; }
        public Tag? Tag { get; set; }

    }
}
