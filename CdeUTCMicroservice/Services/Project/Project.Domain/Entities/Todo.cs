namespace Project.Domain.Entities
{
    public class Todo : Aggregate<TodoId>
    {
        public ProjectId? ProjectId { get; set; }
        public Guid AssignTo { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public Characteristic Characteristic { get; set; } = default!;
        
    }
}
