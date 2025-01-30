

namespace Project.Domain.Entities
{
    public class Status : BaseEntity<StatusId>
    {
        public ProjectId? ProjectId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public string ColorRGB { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Projects? Project { get; set; }


        public static List<Status> InitData(ProjectId projectId)
        {
            return new List<Status>()
            {
                new Status() { Id = StatusId.Of(Guid.NewGuid()), ProjectId = projectId, IsDefault = true, IsActive = true, ColorRGB = "#1E7BD7", Name = "M?i"},
                new Status() { Id = StatusId.Of(Guid.NewGuid()), ProjectId = projectId, IsDefault = false, IsActive = true, ColorRGB = "#28A745", Name = "?ang th?c hi?n" },
                new Status() { Id = StatusId.Of(Guid.NewGuid()), ProjectId = projectId, IsDefault = false, IsActive = true, ColorRGB = "#FFC107", Name = "?ang ch?" },
                new Status() { Id = StatusId.Of(Guid.NewGuid()), ProjectId = projectId, IsDefault = false, IsActive = true, ColorRGB = "#DC3545", Name = "Hoàn thành" },
                new Status() { Id = StatusId.Of(Guid.NewGuid()), ProjectId = projectId, IsDefault = false, IsActive = true, ColorRGB = "#6C757D", Name = "?ã ?óng" },
            };
        }
    }
}
