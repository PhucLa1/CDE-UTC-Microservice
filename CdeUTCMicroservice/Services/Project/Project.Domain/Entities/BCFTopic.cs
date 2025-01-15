
namespace Project.Domain.Entities
{
    public class BCFTopic : Aggregate<BCFTopicId>
    {
        public ProjectId ProjectId { get; private set; } = default!;
        public Guid AssignTo { get; private set; } = default!;
        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public DateTime DueDate { get; private set; }
        public Characteristic Characteristic { get; private set; } = default!;

        public static BCFTopic Create(ProjectId projectId, Guid assignTo, string title, string description, DateTime dueDate, Characteristic characteristic)
        {
            DateTime createdAt = DateTime.UtcNow; // Giá trị mặc định cho thời gian tạo

            if (dueDate < createdAt)
                throw new ArgumentException("Due date cannot be earlier than the creation date.");
            return new BCFTopic
            {
                ProjectId = projectId,
                AssignTo = assignTo,
                Title = title,
                Description = description,
                DueDate = dueDate,
                Characteristic = characteristic
            };
            
        }
    }
}
