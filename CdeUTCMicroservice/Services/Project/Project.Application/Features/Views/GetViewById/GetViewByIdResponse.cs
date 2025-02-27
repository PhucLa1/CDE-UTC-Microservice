using Project.Application.Dtos.Result;

namespace Project.Application.Features.Views.GetViewById
{
    public class GetViewByIdResponse
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public string NameCreatedBy { get; set; } = string.Empty;
        public List<TagResult> TagResults { get; set; } = new List<TagResult>() { };
        //public List<FolderHistoryResult> FolderHistoryResults { get; set; } = new List<FolderHistoryResult>() { };
        public List<UserCommentResult> UserCommentResults { get; set; } = new List<UserCommentResult>() { };
    }
}
