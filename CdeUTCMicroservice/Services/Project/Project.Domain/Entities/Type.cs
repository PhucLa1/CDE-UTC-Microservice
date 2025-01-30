namespace Project.Domain.Entities
{
    public class Type : BaseEntity<TypeId>
    {
        public ProjectId? ProjectId { get; set; } = default!;
        public string IconImageUrl { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Projects? Project { get; set; }
        public static List<Type> InitData(ProjectId projectId)
        {
            return new List<Type>()
            {
                new Type() {Id = TypeId.Of(Guid.NewGuid()), ProjectId = projectId, IconImageUrl = "undefined_icon.png", Name = "Không xác định" }, // Undefined
                new Type() {Id = TypeId.Of(Guid.NewGuid()), ProjectId = projectId, IconImageUrl = "comment_icon.png", Name = "Bình luận" },        // Comment
                new Type() { Id = TypeId.Of(Guid.NewGuid()),ProjectId = projectId, IconImageUrl = "issue_icon.png", Name = "Vấn đề" },            // Issue
                new Type() {Id = TypeId.Of(Guid.NewGuid()), ProjectId = projectId, IconImageUrl = "request_icon.png", Name = "Yêu cầu" },         // Request
                new Type() {Id = TypeId.Of(Guid.NewGuid()), ProjectId = projectId, IconImageUrl = "fault_icon.png", Name = "Lỗi" },              // Fault
                new Type() {Id = TypeId.Of(Guid.NewGuid()), ProjectId = projectId, IconImageUrl = "inquiry_icon.png", Name = "Thắc mắc" },        // Inquiry
                new Type() {Id = TypeId.Of(Guid.NewGuid()), ProjectId = projectId, IconImageUrl = "solution_icon.png", Name = "Giải pháp" },      // Solution
                new Type() {Id = TypeId.Of(Guid.NewGuid()), ProjectId = projectId, IconImageUrl = "remark_icon.png", Name = "Ghi chú" },          // Remark
                new Type() { Id = TypeId.Of(Guid.NewGuid()),ProjectId = projectId, IconImageUrl = "clash_icon.png", Name = "Xung đột" },          // Clash
            };
        }
    }
}
