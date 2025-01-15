namespace Project.Domain.Entities
{
    public class Type : Entity<TypeId>
    {
        public ProjectId ProjectId { get; private set; } = default!;
        public string IconImageUrl { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public static Type Create(ProjectId projectId, string iconImageUrl, string name)
        {
            var type = new Type
            {
                ProjectId = projectId,
                IconImageUrl = iconImageUrl,
                Name = name
            };
            return type;
        }
    }
}
