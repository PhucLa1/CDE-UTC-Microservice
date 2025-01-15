namespace Project.Domain.Entities
{
    public class Group : Entity<GroupId>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public ProjectId ProjectId { get; private set; } = default!;
        public static Group Create(string name, string description, ProjectId projectId)
        {
            ArgumentNullException.ThrowIfNull(name);
            var group = new Group
            {
                Name = name,
                Description = description,
                ProjectId = projectId
            };
            return group;
        }
    }
}
