namespace Project.Application.Features.Comment.ViewComments.DeleteViewComment
{
    public class DeleteViewCommentRequest : ICommand<DeleteViewCommentResponse>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}
