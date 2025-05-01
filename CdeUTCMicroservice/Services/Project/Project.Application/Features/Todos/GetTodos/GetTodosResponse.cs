namespace Project.Application.Features.Todos.GetTodos
{
    public class GetTodosResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IsAssignToGroup { get; set; }
        public int AssignTo { get; set; }
        public int CreatedBy { get; set; }
        public string NameCreatedBy { get; set; } = string.Empty;
        public Priority? Priority { get; set; }
        public Status? Status { get; set; }
        public Type? Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string DueDate { get; set; } = string.Empty;
        public List<Tag>? Tags { get; set; }
    }
}
