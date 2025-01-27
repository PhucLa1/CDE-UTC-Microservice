using Project.Domain.ValueObjects.Id;

namespace Project.Domain.Entities
{
    public class ViewComment : Entity<ViewCommentId>
    {
        public string Content { get; set; } = default!;
        public ViewId? ViewId { get; set; } = default!;
        
    }
}
