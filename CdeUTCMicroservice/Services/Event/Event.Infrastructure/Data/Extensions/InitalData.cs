using Event.Core.Entities;

namespace Auth.Data.Data.Extensions
{
    public static class InitalData
    {
        public static IEnumerable<ActivityTypeParent> ActivityTypeParents => new List<ActivityTypeParent>()
        {
            new ActivityTypeParent()
            {
                Name = "Tệp",
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Tải tệp", Template = "Tệp {FileName} đã được tải lên vào dự án" },
                    new ActivityType(){ Name = "Xóa tệp", Template = "Tệp {FileName} đã bị xóa khỏi dự án" },
                    new ActivityType(){ Name = "Di chuyển tệp", Template = "Tệp {FileName} đã được chuyển từ thư mục {OldFolder} đển thư mục {NewFolder}" },
                    new ActivityType(){ Name = "Đổi tên tệp", Template = "Tệp {OldFileName} đã được đổi tên thành {NewFileName}" },
                    new ActivityType(){ Name = "Tải xuống tệp", Template = "Tệp {FileName} đã được tải xuống" }
                },
                IconImageUrl = "Files.png"
            },
            new ActivityTypeParent()
            {
                Name = "Thư mục",
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Thêm thư mục", Template = "Thư mục {FolderName} đã được thêm vào dự án" },
                    new ActivityType(){ Name = "Xóa thư mục", Template = "Thư mục {FolderName} đã bị xóa khỏi dự án" },
                    new ActivityType(){ Name = "Di chuyển thư mục", Template = "Thư mục {FolderName} đã được chuyển từ thư mục {OldFolder} đển thư mục {NewFolder}" },
                    new ActivityType(){ Name = "Đổi tên thư mục", Template = "Thư mục {OldFolderName} đã được đổi tên thành {NewFolderName}" },
                    new ActivityType(){ Name = "Tải xuống thư mục", Template = "Thư mục {FolderName} đã được tải xuống" }
                },
                IconImageUrl = "Folders.png"
            },
            new ActivityTypeParent()
            {
                Name = "Người dùng",
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Người dùng được mời vào dự án", Template = "{Email} được mời vào dự án" },
                    new ActivityType(){ Name = "Xóa người dùng khỏi dự án", Template = "{UserRemoveEmail} đã xóa {UserRemovedEmail} khỏi dự án" },
                    new ActivityType(){ Name = "Người dùng rời dự án", Template = "{UserEmail} đã rời khỏi dự án" },
                    new ActivityType(){ Name = "Người dùng tham gia dự án", Template = "{UserEmail} đã đồng ý tham gia dự án" }
                },
                IconImageUrl = "Users.png"
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
                IconImageUrl = "Views.png"
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
                IconImageUrl = "Todo.png"
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
                IconImageUrl = "BCFTopic.png"
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
                IconImageUrl = "Comments.png"
            },
            new ActivityTypeParent()
            {
                Name = "Khác",
                ActivityTypes = new List<ActivityType>()
                {
                    new ActivityType(){ Name = "Cập nhật dự án", Template = "Dự án {ProjectName} đã được cập nhật" },
                    new ActivityType(){ Name = "Đổi tên dự án", Template = "Dự án {OldProjectName} đã được đổi tên thành {NewProjectName}" }
                },
                IconImageUrl = "Project.png"
            }
        }
        .OrderBy(x => x.Id) // Sắp xếp theo Id của ActivityTypeParent
        .ToList();
    }
}