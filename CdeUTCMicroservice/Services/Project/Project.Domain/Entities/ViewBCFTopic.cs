using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class ViewBCFTopic : Entity<ViewBCFTopicId>
    {
        public ViewId ViewId { get; private set; } = default!;
        public BCFTopicId BCFTopicId { get; private set; } = default!;
        public static ViewBCFTopic Create(ViewId viewId, BCFTopicId bcfTopicId)
        {
            var viewBCFTopic = new ViewBCFTopic
            {
                ViewId = viewId,
                BCFTopicId = bcfTopicId
            };
            return viewBCFTopic;
        }
    }
}
