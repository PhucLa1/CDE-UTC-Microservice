

using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Team.LeaveProject
{
    public class LeaveProjectHandler
        (IBaseRepository<UserProject> userProjectRepository,
                        IUserGrpc userGrc,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<LeaveProjectRequest, LeaveProjectResponse>
    {
        public async Task<LeaveProjectResponse> Handle(LeaveProjectRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(x => x.UserId == currentUserId && x.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            userProjectRepository.Remove(userProject);
            await userProjectRepository.SaveChangeAsync(cancellationToken);

            var ids = new List<int>() { userProject.UserId };

            var users = await userGrc.GetUsersByIds(new GetUserRequestGrpc() { Ids = ids });


            // Gửi sự kiện ghi nhận việc rời dự án
            var activityEvent = new CreateActivityEvent
            {
                Action = "LEAVE_PROJECT",
                ResourceId = currentUserId,
                Content = $"Người dùng có email {users.First().Email} đã rời khỏi dự án.",
                TypeActivity = TypeActivity.Team,
                ProjectId = request.ProjectId
            };

            await publishEndpoint.Publish(activityEvent, cancellationToken);

            return new LeaveProjectResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
