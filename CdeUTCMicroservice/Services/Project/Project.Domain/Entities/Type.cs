using BuildingBlocks.Extensions;

namespace Project.Domain.Entities
{
    public class Type : BaseEntity<TypeId>
    {
        public ProjectId? ProjectId { get; set; } = default!;
        public string IconImageUrl { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsBlock { get; set; } = false;
        public Projects? Project { get; set; }
        public static List<Type> InitData(ProjectId projectId)
        {
            return new List<Type>()
            {
                new Type() {Id = TypeId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, IconImageUrl = "undefined_icon.png", Name = "Không xác định", IsBlock = true }, // Undefined
                new Type() {Id = TypeId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, IconImageUrl = "comment_icon.png", Name = "Bình luận", IsBlock = true },        // Comment
                new Type() { Id = TypeId.Of(Guid.NewGuid().Sequence()),ProjectId = projectId, IconImageUrl = "issue_icon.png", Name = "Vấn đề" , IsBlock = true},            // Issue
                new Type() {Id = TypeId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, IconImageUrl = "request_icon.png", Name = "Yêu cầu", IsBlock = true },         // Request
                new Type() {Id = TypeId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, IconImageUrl = "fault_icon.png", Name = "Lỗi" , IsBlock = true},              // Fault
                new Type() {Id = TypeId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, IconImageUrl = "inquiry_icon.png", Name = "Thắc mắc", IsBlock = true },        // Inquiry
                new Type() {Id = TypeId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, IconImageUrl = "solution_icon.png", Name = "Giải pháp", IsBlock = true },      // Solution
                new Type() {Id = TypeId.Of(Guid.NewGuid().Sequence()), ProjectId = projectId, IconImageUrl = "remark_icon.png", Name = "Ghi chú", IsBlock = true },          // Remark
                new Type() { Id = TypeId.Of(Guid.NewGuid().Sequence()),ProjectId = projectId, IconImageUrl = "clash_icon.png", Name = "Xung đột", IsBlock = true },          // Clash
            };
        }
    }
}
