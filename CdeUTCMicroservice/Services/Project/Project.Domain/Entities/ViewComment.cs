using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class ViewComment : Entity<ViewCommentId>
    {
        public string Content { get; private set; } = default!;
        public ViewId ViewId { get; private set; } = default!;
        public static ViewComment Create(string content, ViewId viewId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(content);
            var viewComment = new ViewComment
            {
                Content = content,
                ViewId = viewId
            };
            return viewComment;
        }
    }
}
