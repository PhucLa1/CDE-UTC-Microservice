namespace Project.Application.Features.Comment.FileComments.CreateFileComment
{
    public class CreateFileCommentRequest : ICommand<CreateFileCommentResponse>
    {
        public string Content { get; set; } = string.Empty;
        public int FileId { get; set; }
    }
}
