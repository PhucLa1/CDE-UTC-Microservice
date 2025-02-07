namespace Project.Domain.Entities
{
    public class ViewComment : BaseEntity
    {
        public string Content { get; set; } = default!;
        public int? ViewId { get; set; } = default!;
        
    }
}
