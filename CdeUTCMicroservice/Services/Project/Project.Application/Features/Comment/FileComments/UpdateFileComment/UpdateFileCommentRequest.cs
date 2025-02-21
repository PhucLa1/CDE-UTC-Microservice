namespace Project.Application.Features.Comment.FileComments.UpdateFileComment
{
    public class UpdateFileCommentRequest : ICommand<UpdateFileCommentResponse>
    {
        public string Content { get; set; } = string.Empty;
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}
