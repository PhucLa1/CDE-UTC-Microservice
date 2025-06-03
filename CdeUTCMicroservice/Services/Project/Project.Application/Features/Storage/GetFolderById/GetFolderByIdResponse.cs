using Project.Application.Dtos.Result;

namespace Project.Application.Features.Storage.GetFolderById
{
    public class GetFolderByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public string NameCreatedBy { get; set; } = string.Empty;
        public Access Access { get; set; }
        public List<TagResult> TagResults { get; set; } = new List<TagResult>() { };
        public List<FolderHistoryResult> FolderHistoryResults { get; set; } = new List<FolderHistoryResult>() { };
        public List<UserCommentResult> UserCommentResults { get; set; } = new List<UserCommentResult>() { };
        public List<StoragePermissionResult> StoragePermissionResults { get; set; } = new List<StoragePermissionResult>() { };
    }
}
