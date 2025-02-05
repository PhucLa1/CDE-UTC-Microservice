using BuildingBlocks.Extensions;

namespace Project.Domain.Entities
{
    public class Priority : BaseEntity<PriorityId>
    {
        public ProjectId? ProjectId { get; set; }
        public string ColorRGB { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsBlock { get; set; } = false;

        public Projects? Project { get; set; }
        public static List<Priority> InitData(ProjectId projectId)
        {
            return new List<Priority>()
            {
                new Priority() {Id = PriorityId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, ColorRGB = "#DC3545", Name = "Cao", IsBlock = true },      // Priority cao
                new Priority() {Id = PriorityId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, ColorRGB = "#FFC107", Name = "Trung bình", IsBlock = true }, // Priority trung bình
                new Priority() {Id = PriorityId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, ColorRGB = "#28A745", Name = "Thấp", IsBlock = true },     // Priority thấp
                new Priority() {Id = PriorityId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, ColorRGB = "#007BFF", Name = "Bình thường", IsBlock = true }, // Priority mặc định
                new Priority() {Id = PriorityId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, ColorRGB = "#6C757D", Name = "Không ưu tiên", IsBlock = true }, // Priority không ưu tiên
            };
        }
    }
}
