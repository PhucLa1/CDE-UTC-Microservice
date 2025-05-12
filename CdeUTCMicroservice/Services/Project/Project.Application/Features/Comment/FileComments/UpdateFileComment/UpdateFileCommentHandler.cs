using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Transports;
using Project.Domain.Entities;

namespace Project.Application.Features.Comment.FileComments.UpdateFileComment
{
    public class UpdateFileCommentHandler
     (IBaseRepository<FileComment> fileCommentRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<File> fileRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateFileCommentRequest, UpdateFileCommentResponse>
    {
        public async Task<UpdateFileCommentResponse> Handle(UpdateFileCommentRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            var fileComment = await fileCommentRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || fileComment is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin && fileComment.CreatedBy != userCurrentId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var oldContent = fileComment.Content;
            fileComment.Content = request.Content;
            fileCommentRepository.Update(fileComment);
            await fileCommentRepository.SaveChangeAsync(cancellationToken);

            var file = await fileRepository.GetAllQueryAble().FirstAsync(e => e.Id == fileComment.FileId);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "UPDATE",
                ResourceId = fileComment.Id,
                Content = "Đã sửa mới binh luận từ nội dung " + oldContent + " sang thành " + request.Content + " ở tệp " + file.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = request.ProjectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new UpdateFileCommentResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
