namespace Project.Domain.Entities
{
    public class TodoTag : Entity<TodoTagId>
    {
        public TodoId? TodoId { get; set; }
        public TagId? TagId { get; set; }
        
    }
}
