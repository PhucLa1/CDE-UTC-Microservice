
namespace Project.Application.Features.Comment.FolderComments.CreateFolderComment
{
    public class CreateFolderCommentHandler
        (IBaseRepository<FolderComment> folderCommentRepository)
        : ICommandHandler<CreateFolderCommentRequest, CreateFolderCommentResponse>
    {
        public async Task<CreateFolderCommentResponse> Handle(CreateFolderCommentRequest request, CancellationToken cancellationToken)
        {
            var folderComment = new FolderComment()
            {
                FolderId = request.FolderId,
                Content = request.Content,
            };

            await folderCommentRepository.AddAsync(folderComment, cancellationToken);
            await folderCommentRepository.SaveChangeAsync(cancellationToken);
            return new CreateFolderCommentResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
