namespace Project.Domain.Entities
{
    public class Todo : Aggregate<TodoId>
    {
        public ProjectId ProjectId { get; private set; } = default!;
        public Guid AssignTo { get; private set; }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public DateTime DueDate { get; private set; }
        public Characteristic Characteristic { get; set; } = default!;
        public static Todo Create(ProjectId projectId, Guid assignTo, string name, string description, DateTime dueDate, Characteristic characteristic)
        {
            DateTime createdAt = DateTime.UtcNow; // Giá trị mặc định cho thời gian tạo

            if (dueDate < createdAt)
                throw new ArgumentException("Due date cannot be earlier than the creation date.");
            var todo = new Todo
            {
                ProjectId = projectId,
                AssignTo = assignTo,
                Name = name,
                Description = description,
                DueDate = dueDate,
                Characteristic = characteristic
            };
            return todo;
        }
    }
}
