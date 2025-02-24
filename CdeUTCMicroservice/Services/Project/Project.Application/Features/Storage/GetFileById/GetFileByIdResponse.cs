using Project.Application.Dtos.Result;

namespace Project.Application.Storage.GetFileById
{
    public class GetFileByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public string NameCreatedBy { get; set; } = string.Empty;
        public List<TagResult> TagResults { get; set; } = new List<TagResult>() { };
        public List<FileHistoryResult> FileHistoryResults { get; set; } = new List<FileHistoryResult>() { };
        public List<UserCommentResult> UserCommentResults { get; set; } = new List<UserCommentResult>() { };
    }
}
