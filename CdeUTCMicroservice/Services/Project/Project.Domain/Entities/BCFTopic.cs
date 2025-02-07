
namespace Project.Domain.Entities
{
    public class BCFTopic : BaseEntity
    {
        public int? ProjectId { get; set; } = default!;
        public int AssignTo { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public Characteristic Characteristic { get; set; } = new Characteristic();
        public ICollection<BCFComment>? BCFComments { get; set; }
        public ICollection<BCFTopicTag>? BCFTopicTags { get; set; }
        public Projects? Project { get; set; }

    }
}
