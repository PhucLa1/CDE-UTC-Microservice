namespace Project.Domain.Entities
{
    public class Priority : BaseEntity
    {
        public int? ProjectId { get; set; }
        public string ColorRGB { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsBlock { get; set; } = false;

        public Projects? Project { get; set; }
        public static List<Priority> InitData(int projectId)
        {
            return new List<Priority>()
            {
                new Priority() {ProjectId = projectId, ColorRGB = "#DC3545", Name = "Cao", IsBlock = true },      // Priority cao
                new Priority() {ProjectId = projectId, ColorRGB = "#FFC107", Name = "Trung bình", IsBlock = true }, // Priority trung bình
                new Priority() {ProjectId = projectId, ColorRGB = "#28A745", Name = "Thấp", IsBlock = true },     // Priority thấp
                new Priority() {ProjectId = projectId, ColorRGB = "#007BFF", Name = "Bình thường", IsBlock = true }, // Priority mặc định
                new Priority() {ProjectId = projectId, ColorRGB = "#6C757D", Name = "Không ưu tiên", IsBlock = true }, // Priority không ưu tiên
            };
        }
    }
}
