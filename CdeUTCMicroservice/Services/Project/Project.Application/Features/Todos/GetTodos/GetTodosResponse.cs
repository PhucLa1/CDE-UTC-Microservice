namespace Project.Application.Features.Todos.GetTodos
{
    public class GetTodosResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IsAssign { get; set; }
        public int IsAssignToGroup { get; set; }
        public string AssignTo { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string NameCreatedBy { get; set; } = string.Empty;
        public int PriorityId { get; set; }
        public string PriorityName { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }
}
