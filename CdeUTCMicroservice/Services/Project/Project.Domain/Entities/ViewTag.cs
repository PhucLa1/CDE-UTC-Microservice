using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class ViewTag : Entity<ViewTagId>
    {
        public ViewId ViewId { get; private set; } = default!;
        public TagId TagId { get; private set; } = default!;
        public static ViewTag Create(ViewId viewId, TagId tagId)
        {
            var viewTag = new ViewTag
            {
                ViewId = viewId,
                TagId = tagId
            };
            return viewTag;
        }
    }
}
