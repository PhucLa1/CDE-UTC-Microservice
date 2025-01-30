namespace Project.Domain.Entities
{
    public class Priority : BaseEntity<PriorityId>
    {
        public ProjectId? ProjectId { get; set; }
        public string ColorRGB { get; set; } = default!;
        public string Name { get; set; } = default!;

        public Projects? Project { get; set; }
        public static List<Priority> InitData(ProjectId projectId)
        {
            return new List<Priority>()
            {
                new Priority() {Id = PriorityId.Of(Guid.NewGuid()), ProjectId = projectId, ColorRGB = "#DC3545", Name = "Cao" },      // Priority cao
                new Priority() {Id = PriorityId.Of(Guid.NewGuid()), ProjectId = projectId, ColorRGB = "#FFC107", Name = "Trung bình" }, // Priority trung bình
                new Priority() {Id = PriorityId.Of(Guid.NewGuid()), ProjectId = projectId, ColorRGB = "#28A745", Name = "Thấp" },     // Priority thấp
                new Priority() {Id = PriorityId.Of(Guid.NewGuid()), ProjectId = projectId, ColorRGB = "#007BFF", Name = "Bình thường" }, // Priority mặc định
                new Priority() {Id = PriorityId.Of(Guid.NewGuid()), ProjectId = projectId, ColorRGB = "#6C757D", Name = "Không ưu tiên" }, // Priority không ưu tiên
            };
        }
    }
}
