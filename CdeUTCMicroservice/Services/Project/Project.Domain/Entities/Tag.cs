namespace Project.Domain.Entities
{
    public class Tag : Entity<TagId>
    {
        public ProjectId ProjectId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string DataNearest { get; set; } = default!;
        public static Tag Create(ProjectId projectId, string name, string dataNearest)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            var tag = new Tag
            {
                ProjectId = projectId,
                Name = name,
                DataNearest = dataNearest
            };
            return tag;
        }
    }
}
