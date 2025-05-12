
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Team.ApproveInvite
{
    public class ApproveInviteHandler
        (IBaseRepository<UserProject> userProjectRepository,
        IUserGrpc userGrc,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<ApproveInviteRequest, ApproveInviteResponse>
    {
        public async Task<ApproveInviteResponse> Handle(ApproveInviteRequest request, CancellationToken cancellationToken)
        {
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == request.UserId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            userProject.UserProjectStatus = UserProjectStatus.Active;
            userProjectRepository.Update(userProject);
            await userProjectRepository.SaveChangeAsync(cancellationToken);

            var ids = new List<int>() { userProject.UserId };

            var users = await userGrc.GetUsersByIds(new GetUserRequestGrpc() { Ids = ids });


            var activityEvent = new CreateActivityEvent
            {
                Action = "APPROVE_INVITE",
                ResourceId = userProject.UserId,
                Content = $"Người dùng ${users.First().Email} đã chấp nhận lời mời tham gia dự án.",
                TypeActivity = TypeActivity.Team,
                ProjectId = userProject.ProjectId.Value
            };

            await publishEndpoint.Publish(activityEvent, cancellationToken);
            return new ApproveInviteResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
