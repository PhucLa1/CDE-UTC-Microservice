namespace Project.Application.Features.Comment.FolderComments.DeleteFolderComment
{
    public class DeleteFolderCommentRequest : ICommand<DeleteFolderCommentResponse>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}
