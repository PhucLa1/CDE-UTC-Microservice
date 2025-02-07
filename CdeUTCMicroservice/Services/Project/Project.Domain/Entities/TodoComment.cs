namespace Project.Domain.Entities
{
    public class TodoComment : BaseEntity
    {
        public string Content { get; set; } = default!;
        public int? TodoId { get; set; }

    }
}
