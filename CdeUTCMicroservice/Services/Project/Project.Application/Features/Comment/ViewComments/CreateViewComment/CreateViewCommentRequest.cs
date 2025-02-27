namespace Project.Application.Features.Comment.ViewComments.CreateViewComment
{
    public class CreateViewCommentRequest : ICommand<CreateViewCommentResponse>
    {
        public int ViewId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
