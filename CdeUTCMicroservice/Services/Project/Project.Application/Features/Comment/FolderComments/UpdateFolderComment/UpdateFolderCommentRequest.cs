namespace Project.Application.Features.Comment.FolderComments.UpdateFolderComment
{
    public class UpdateFolderCommentRequest
    {
        public string Content { get; set; } = string.Empty;
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}
