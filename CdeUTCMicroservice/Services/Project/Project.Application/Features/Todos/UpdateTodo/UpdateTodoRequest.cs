namespace Project.Application.Features.Todos.UpdateTodo
{
    public class UpdateTodoRequest : ICommand<UpdateTodoResponse>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int? TypeId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
        public int IsAssignToGroup { get; set; } //0: không, 1: có
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int AssignTo { get; set; }
        public List<int>? FileIds { get; set; }
        public List<int>? ViewIds { get; set; }
        public List<int>? TagIds { get; set; }
    }
}
