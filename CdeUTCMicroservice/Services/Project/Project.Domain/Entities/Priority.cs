namespace Project.Domain.Entities
{
    public class Priority : Entity<PriorityId>
    {
        public ProjectId ProjectId { get; private set; } = default!;
        public string ColorRGB { get; private set; } = default!;
        public string Name { get; private set; } = default!;

        public static Priority Create(ProjectId projectId, string colorRGB, string name)
        {
            var priority = new Priority
            {
                ProjectId = projectId,
                ColorRGB = colorRGB,
                Name = name
            };
            return priority;
        }
    }
}
