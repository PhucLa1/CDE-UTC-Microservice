namespace Project.Domain.Entities
{
    public class TodoTag : BaseEntity<TodoTagId>
    {
        public TodoId? TodoId { get; set; }
        public TagId? TagId { get; set; }
        
    }
}
