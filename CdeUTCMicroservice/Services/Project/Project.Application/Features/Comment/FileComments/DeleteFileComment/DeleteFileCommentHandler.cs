using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Transports;
using Project.Domain.Entities;

namespace Project.Application.Features.Comment.FileComments.DeleteFileComment
{
    public class DeleteFileCommentHandler
    (IBaseRepository<FileComment> fileCommentRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<File> fileRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<DeleteFileCommentRequest, DeleteFileCommentResponse>
    {
        public async Task<DeleteFileCommentResponse> Handle(DeleteFileCommentRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var projectId = request.ProjectId;
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            var fileComment = await fileCommentRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || fileComment is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin && fileComment.CreatedBy != userCurrentId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            fileCommentRepository.Remove(fileComment);
            await fileCommentRepository.SaveChangeAsync(cancellationToken);

            var file = await fileRepository.GetAllQueryAble().FirstAsync(e => e.Id == fileComment.FileId);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = fileComment.Id,
                Content = "Đã xóa bình luận với nội dung " + fileComment.Content + " ở tệp " + file.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = projectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new DeleteFileCommentResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
