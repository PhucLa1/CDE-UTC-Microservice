namespace Project.Application.Features.Comment.FolderComments.CreateFolderComment
{
    public class CreateFolderCommentRequest : ICommand<CreateFolderCommentResponse>
    {
        public string Content { get; set; } = string.Empty;
        public int FolderId { get; set; }
    }
}
