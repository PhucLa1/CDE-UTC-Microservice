namespace Project.Application.Features.Comment.FileComments.DeleteFileComment
{
    public class DeleteFileCommentRequest : ICommand<DeleteFileCommentResponse>   
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}
