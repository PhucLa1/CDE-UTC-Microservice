namespace Project.Domain.Entities
{
    public class Todo : BaseEntity
    {
        public int? ProjectId { get; set; }
        public int AssignTo { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public Characteristic Characteristic { get; set; } = new Characteristic();
        
    }
}
