

namespace Project.Domain.Entities
{
    public class Status : Entity<StatusId>
    {
        public ProjectId ProjectId { get; private set; } = default!;
        public bool IsDefault { get; private set; }
        public bool IsActive { get; private set; }
        public string ColorRGB { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public static Status Create(ProjectId projectId, bool isDefault, bool isActive, string colorRGB, string name)
        {
            var status = new Status
            {
                ProjectId = projectId,
                IsDefault = isDefault,
                IsActive = isActive,
                ColorRGB = colorRGB,
                Name = name
            };
            return status;
        }
    }
}
