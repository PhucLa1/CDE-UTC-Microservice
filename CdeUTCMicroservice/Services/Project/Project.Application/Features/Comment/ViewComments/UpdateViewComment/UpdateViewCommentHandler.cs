using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Comment.ViewComments.UpdateViewComment
{
    public class UpdateViewCommentHandler
    (IBaseRepository<ViewComment> viewCommentRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<View> viewRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateViewCommentRequest, UpdateViewCommentResponse>
    {
        public async Task<UpdateViewCommentResponse> Handle(UpdateViewCommentRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            var viewComment = await viewCommentRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || viewComment is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin && viewComment.CreatedBy != userCurrentId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var oldComment = viewComment.Content;
            viewComment.Content = request.Content;
            viewCommentRepository.Update(viewComment);
            await viewCommentRepository.SaveChangeAsync(cancellationToken);

            var view = await viewRepository.GetAllQueryAble().FirstAsync(e => e.Id == viewComment.ViewId);
            var eventMessage = new CreateActivityEvent()
            {
                Action = "UPDATE",
                ResourceId = viewComment.Id,
                Content = "Đã sửa bình luận từ nội dung " + oldComment + " sang nội dung " + request.Content + " ở góc nhìn " + view.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = request.ProjectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new UpdateViewCommentResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
