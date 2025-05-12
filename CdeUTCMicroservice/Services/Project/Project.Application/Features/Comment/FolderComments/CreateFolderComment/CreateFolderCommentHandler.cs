
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Comment.FolderComments.CreateFolderComment
{
    public class CreateFolderCommentHandler
        (IBaseRepository<FolderComment> folderCommentRepository,
        IBaseRepository<Folder> folderRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<CreateFolderCommentRequest, CreateFolderCommentResponse>
    {
        public async Task<CreateFolderCommentResponse> Handle(CreateFolderCommentRequest request, CancellationToken cancellationToken)
        {
            var projectId = folderCommentRepository.GetProjectId();
            var folderComment = new FolderComment()
            {
                FolderId = request.FolderId,
                Content = request.Content,
            };

            await folderCommentRepository.AddAsync(folderComment, cancellationToken);
            await folderCommentRepository.SaveChangeAsync(cancellationToken);

            var folder = await folderRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.FolderId);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = folderComment.Id,
                Content = "Đã tạo bình luận với nội dung " + request.Content + " ở thư mục " + folder.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = projectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);
            return new CreateFolderCommentResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
