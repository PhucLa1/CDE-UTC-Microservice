using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class ViewBCFTopic : Entity<ViewBCFTopicId>
    {
        public ViewId? ViewId { get; set; } = default!;
        public BCFTopicId? BCFTopicId { get; set; } = default!;
       
    }
}
