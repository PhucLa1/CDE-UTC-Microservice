using Event.Core.Entities.Base;
using Event.Core.Enums;

namespace Event.Core.Entities
{
    public class ActivityTypeParent : BaseEntity
    {
        public ActivityTypeParentCategory ActivityTypeParentCategory { get; set; }
        public string Name { get; set; } = default!;
        public string IconImageUrl { get; set; } = default!;
        public int ProjectId { get; set; }
        public ICollection<ActivityType> ActivityTypes { get; set; } = new List<ActivityType>();
        public static IEnumerable<ActivityTypeParent> ActivityTypeParents(int projectId) => new List<ActivityTypeParent>()
        {
            new ActivityTypeParent()
            {
                Name = "Tệp",
                ActivityTypeParentCategory = ActivityTypeParentCategory.File,
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Tải tệp", Template = "Tệp {FileName} đã được tải lên vào dự án" },
                    new ActivityType(){ Name = "Xóa tệp", Template = "Tệp {FileName} đã bị xóa khỏi dự án" },
                    new ActivityType(){ Name = "Di chuyển tệp", Template = "Tệp {FileName} đã được chuyển từ thư mục {OldFolder} đến thư mục {NewFolder}" },
                    new ActivityType(){ Name = "Đổi tên tệp", Template = "Tệp {OldFileName} đã được đổi tên thành {NewFileName}" },
                    new ActivityType(){ Name = "Tải xuống tệp", Template = "Tệp {FileName} đã được tải xuống" }
                },
                IconImageUrl = "Files.png",
                ProjectId = projectId
            },
            new ActivityTypeParent()
            {
                Name = "Thư mục",
                ActivityTypeParentCategory = ActivityTypeParentCategory.Folder,
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Thêm thư mục", Template = "Thư mục {FolderName} đã được thêm vào dự án" },
                    new ActivityType(){ Name = "Xóa thư mục", Template = "Thư mục {FolderName} đã bị xóa khỏi dự án" },
                    new ActivityType(){ Name = "Di chuyển thư mục", Template = "Thư mục {FolderName} đã được chuyển từ thư mục {OldFolder} đến thư mục {NewFolder}" },
                    new ActivityType(){ Name = "Đổi tên thư mục", Template = "Thư mục {OldFolderName} đã được đổi tên thành {NewFolderName}" },
                    new ActivityType(){ Name = "Tải xuống thư mục", Template = "Thư mục {FolderName} đã được tải xuống" }
                },
                IconImageUrl = "Folders.png",
                ProjectId = projectId
            },
            new ActivityTypeParent()
            {
                Name = "Đội nhóm",
                ActivityTypeParentCategory = ActivityTypeParentCategory.Team,
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Người dùng được mời vào dự án", Template = "{Email} được mời vào dự án", ActivityTypeCategory = ActivityTypeCategory.InviteUser },
                    new ActivityType(){ Name = "Xóa người dùng khỏi dự án", Template = "{UserRemoveEmail} đã xóa {UserRemovedEmail} khỏi dự án", ActivityTypeCategory = ActivityTypeCategory.RemoveUser },
                    new ActivityType(){ Name = "Người dùng rời dự án", Template = "{UserEmail} đã rời khỏi dự án", ActivityTypeCategory = ActivityTypeCategory.LeaveProject },
                    new ActivityType(){ Name = "Người dùng tham gia dự án", Template = "{UserEmail} đã đồng ý tham gia dự án", ActivityTypeCategory = ActivityTypeCategory.JoinProject },
                    new ActivityType(){ Name = "Thêm người dùng vào nhóm", Template = "{UserEmail} đã đồng ý tham gia dự án", ActivityTypeCategory = ActivityTypeCategory.AddUserInGroup },
                    new ActivityType(){ Name = "Tạo mới nhóm", Template = "{UserEmail} đã đồng ý tham gia dự án", ActivityTypeCategory = ActivityTypeCategory.AddUserInGroup },
                    new ActivityType(){ Name = "Xóa người dùng ra khỏi nhóm", Template = "{UserEmail} đã đồng ý tham gia dự án", ActivityTypeCategory = ActivityTypeCategory.RemoveUserInGroup },
                    new ActivityType(){ Name = "Thay đổi tên nhóm", Template = "{UserEmail} đã đồng ý tham gia dự án", ActivityTypeCategory = ActivityTypeCategory.UpdateGroup },
                    new ActivityType(){ Name = "Xóa nhóm", Template = "{UserEmail} đã đồng ý tham gia dự án", ActivityTypeCategory = ActivityTypeCategory.DeleteGroup },
                    new ActivityType(){ Name = "Thay đổi quyền", Template = "{UserEmail} đã đồng ý tham gia dự án", ActivityTypeCategory = ActivityTypeCategory.ChangeRole },
                },
                IconImageUrl = "Team.png",
                ProjectId = projectId
            },
            new ActivityTypeParent()
            {
                Name = "Giao diện hiển thị",
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Thêm giao diện hiển thị", Template = "Giao diện hiển thị {ViewName} được thêm vào" },
                    new ActivityType(){ Name = "Xóa giao diện hiển thị", Template = "Giao diện hiển thị {ViewName} bị xóa khỏi dự án" },
                    new ActivityType(){ Name = "Phân công giao diện hiển thị", Template = "Giao diện hiển thị được phân công {TodoOrBcfTopics}" }
                },
                IconImageUrl = "Views.png",
                ProjectId = projectId
            },
            new ActivityTypeParent()
            {
                Name = "Việc cần làm",
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Thêm mới việc cần làm", Template = "Việc cần làm {ToDoName} đã được thêm vào" },
                    new ActivityType(){ Name = "Chỉnh sửa việc cần làm", Template = "Việc cần làm {ToDoName} đã được chỉnh sửa" },
                    new ActivityType(){ Name = "Xóa việc cần làm", Template = "Việc cần làm {ToDoName} đã bị xóa" },
                    new ActivityType(){ Name = "Thêm tệp vào việc cần làm", Template = "Tệp tin {FileNames} đã được thêm vào việc cần làm {ToDoName}" },
                    new ActivityType(){ Name = "Tệp liên quan đến việc cần làm đã bị xóa", Template = "Tệp tin {FileName} liên kết với việc cần làm {ToDoName} đã bị xóa" },
                    new ActivityType(){ Name = "Nhiệm vụ việc cần làm", Template = "Việc cần làm {ToDoName} đã được phân công tới {AssignedUser}" }
                },
                IconImageUrl = "Todo.png",
                ProjectId = projectId
            },
            new ActivityTypeParent()
            {
                Name = "Chủ đề BCF",
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Thêm mới chủ đề BCF", Template = "Chủ đề BCF {BcfTopicName} đã được thêm vào" },
                    new ActivityType(){ Name = "Chỉnh sửa chủ đề BCF", Template = "Chủ đề BCF {BcfTopicName} đã được chỉnh sửa" },
                    new ActivityType(){ Name = "Xóa chủ đề BCF", Template = "Chủ đề BCF {BcfTopicName} đã bị xóa" },
                    new ActivityType(){ Name = "Thêm tệp vào chủ đề BCF", Template = "Tệp tin {FileNames} đã được thêm vào chủ đề BCF {BcfTopicName}" },
                    new ActivityType(){ Name = "Tệp liên quan đến chủ đề BCF đã bị xóa", Template = "Tệp tin {FileName} liên kết với chủ đề BCF {BcfTopicName} đã bị xóa" },
                    new ActivityType(){ Name = "Nhiệm vụ chủ đề BCF", Template = "Chủ đề BCF {BcfTopicName} đã được phân công tới {AssignedUser}" }
                },
                IconImageUrl = "BCFTopic.png",
                ProjectId = projectId
            },
            new ActivityTypeParent()
            {
                Name = "Bình luận",
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Thêm bình luận", Template = "Bình luận {CommentContent} đã được thêm vào {TypeName}" },
                    new ActivityType(){ Name = "Chỉnh sửa bình luận", Template = "Bình luận {CommentContent} đã được chỉnh sửa" },
                    new ActivityType(){ Name = "Xóa bình luận", Template = "Bình luận {CommentContent} đã bị xóa" }
                },
                IconImageUrl = "Comments.png",
                ProjectId = projectId
            },
            new ActivityTypeParent()
            {
                Name = "Khác",
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Cập nhật dự án", Template = "Dự án {ProjectName} đã được cập nhật" },
                    new ActivityType(){ Name = "Đổi tên dự án", Template = "Dự án {OldProjectName} đã được đổi tên thành {NewProjectName}" }
                },
                IconImageUrl = "Project.png",
                ProjectId = projectId
            }
        }
       .OrderBy(x => x.Id)
       .ToList();
    }
}
