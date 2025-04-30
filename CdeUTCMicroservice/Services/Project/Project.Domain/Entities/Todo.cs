namespace Project.Domain.Entities
{
    public class Todo : BaseEntity
    {
        public int? ProjectId { get; set; }
        public int IsAssignToGroup { get; set; }
        public int AssignTo { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public int? TypeId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
    }
}
