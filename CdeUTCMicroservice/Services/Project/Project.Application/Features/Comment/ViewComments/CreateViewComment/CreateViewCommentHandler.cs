
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Comment.ViewComments.CreateViewComment
{
    public class CreateViewCommentHandler
        (IBaseRepository<ViewComment> viewCommentRepository,
        IBaseRepository<View> viewRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<CreateViewCommentRequest, CreateViewCommentResponse>
    {
        public async Task<CreateViewCommentResponse> Handle(CreateViewCommentRequest request, CancellationToken cancellationToken)
        {
            var viewComment = new ViewComment()
            {
                ViewId = request.ViewId,
                Content = request.Content,
            };

            await viewCommentRepository.AddAsync(viewComment, cancellationToken);
            await viewCommentRepository.SaveChangeAsync(cancellationToken);

            var view = await viewRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.ViewId);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = viewComment.Id,
                Content = "Đã tạo bình luận với nội dung " + request.Content + " ở góc nhìn " + view.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = viewCommentRepository.GetProjectId(),
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new CreateViewCommentResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
