using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Project.Domain.Entities;

namespace Project.Application.Features.Comment.ViewComments.DeleteViewComment
{
    public class DeleteViewCommentHandler
    (IBaseRepository<ViewComment> viewCommentRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<View> viewRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<DeleteViewCommentRequest, DeleteViewCommentResponse>
    {
        public async Task<DeleteViewCommentResponse> Handle(DeleteViewCommentRequest request, CancellationToken cancellationToken)
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

            viewCommentRepository.Remove(viewComment);
            await viewCommentRepository.SaveChangeAsync(cancellationToken);

            var view = await viewRepository.GetAllQueryAble().FirstAsync(e => e.Id == viewComment.ViewId);

            var eventMessage = new CreateActivityEvent()
            {
                Action = "DELETE",
                ResourceId = viewComment.Id,
                Content = "Đã xóa bình luận với nội dung " + viewComment.Content + " ở góc nhìn " + view.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = request.ProjectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new DeleteViewCommentResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
