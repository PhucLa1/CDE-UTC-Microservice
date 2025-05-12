
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Comment.FolderComments.UpdateFolderComment
{
    public class UpdateFolderCommentHandler
        (IBaseRepository<FolderComment> folderCommentRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<Folder> folderRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateFolderCommentRequest, UpdateFolderCommentResponse>
    {
        public async Task<UpdateFolderCommentResponse> Handle(UpdateFolderCommentRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            var folderComment = await folderCommentRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || folderComment is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin && folderComment.CreatedBy != userCurrentId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var oldComment = folderComment.Content;
            folderComment.Content = request.Content;
            folderCommentRepository.Update(folderComment);
            await folderCommentRepository.SaveChangeAsync(cancellationToken);

            var folder = await folderRepository.GetAllQueryAble().FirstAsync(e => e.Id == folderComment.FolderId);
            var eventMessage = new CreateActivityEvent()
            {
                Action = "UPDATE",
                ResourceId = folderComment.Id,
                Content = "Đã sửa bình luận từ nội dung " + oldComment + " sang nội dung " + request.Content + " ở thư mục " + folder.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = request.ProjectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new UpdateFolderCommentResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
