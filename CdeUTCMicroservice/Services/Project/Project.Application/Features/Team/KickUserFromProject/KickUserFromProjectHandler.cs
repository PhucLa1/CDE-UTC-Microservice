
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Team.KickUserFromProject
{
    public class KickUserFromProjectHandler
        (IBaseRepository<UserProject> userProjectRepository,
                IUserGrpc userGrc,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<KickUserFromProjectRequest, KickUserFromProjectResponse>
    {
        public async Task<KickUserFromProjectResponse> Handle(KickUserFromProjectRequest request, CancellationToken cancellationToken)
        {
            //Nếu người xóa không phải là admin
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin && request.UserId != userCurrentId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var userProjectDelete = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == request.UserId && e.ProjectId == request.ProjectId);

            if (userProjectDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            userProjectRepository.Remove(userProjectDelete);
            await userProjectRepository.SaveChangeAsync(cancellationToken);

            var ids = new List<int>() { userProject.UserId };

            var users = await userGrc.GetUsersByIds(new GetUserRequestGrpc() { Ids = ids });

            var activityEvent = new CreateActivityEvent
            {
                Action = "KICK_USER",
                ResourceId = request.UserId,
                Content = $"Đã xóa người dùng có email {users.First().Email} khỏi dự án.",
                TypeActivity = TypeActivity.Team,
                ProjectId = request.ProjectId
            };

            await publishEndpoint.Publish(activityEvent, cancellationToken);
            return new KickUserFromProjectResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
