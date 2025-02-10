using BuildingBlocks.Messaging.Events;
using MassTransit;
using Nest;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Team.InviteUser
{
    public class InviteUserHandler
        (IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<ProjectEntity> projectEntityRepository,
        IUserGrpc userGrpc,
        IPublishEndpoint publishEndpoint
        )
        : ICommandHandler<InviteUserRequest, InviteUserResponse>
    {
        public async Task<InviteUserResponse> Handle(InviteUserRequest request, CancellationToken cancellationToken)
        {
            //Check xem quyền dự án có cho member mời không
            var currentUserId = userProjectRepository.GetCurrentId();
            var userProject = await (from up in userProjectRepository.GetAllQueryAble()
                                     join pe in projectEntityRepository.GetAllQueryAble() on up.ProjectId equals pe.Id
                                     where up.UserId == currentUserId && pe.Id == request.ProjectId
                                     select new
                                     {
                                         UserId = up.UserId,
                                         Role = up.Role,
                                         InvitationPermission = pe.InvitationPermission
                                     }).FirstOrDefaultAsync();

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            //Nếu vừa là member và dự án không cho mời người bên ngoài
            if(userProject.Role == Role.Member && userProject.InvitationPermission == InvitationPermission.OnlyAdminCanInvite)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);


            //Check xem email có trong đó không
            //Lấy ra user
            var user = (await userGrpc
                .GetUsersByIds(new GetUserRequestGrpc() { Ids = new List<int> { userProject.UserId } }))
                .First();

            //

            //Gửi message cho service event
            var invitationUser = new InvitationUserEvent()
            {
                FullName = user.FullName,
                Email = user.Email,
                Role = userProject.Role == Role.Admin ? "Quản trị viên" : "Người dùng"
            };

            await publishEndpoint.Publish(invitationUser, cancellationToken);
            return new InviteUserResponse() { Data = true, Message = Message.INVITATION_USER };

        }
    }
}
