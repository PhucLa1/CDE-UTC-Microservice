namespace Project.Application.Features.Comment.ViewComments.UpdateViewComment
{
    public class UpdateViewCommentRequest : ICommand<UpdateViewCommentResponse>
    {
        public string Content { get; set; } = string.Empty;
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}
