using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using Grpc.Core;
using MassTransit;
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
            //Lấy ra user có email như thế(Nếu không có email như thế thì báo lỗi là không có email)
            try
            {
                var user = await userGrpc.GetUserByEmail(new GetUserByEmailRequestGrpc() { Email = request.Email });


                //Check tiếp là người dùng đó đã tồn tại trong dự án chưa
                var userInvite = await userProjectRepository.GetAllQueryAble()
                    .FirstOrDefaultAsync(e => e.UserId == user.Id , cancellationToken);

                if (userInvite is not null && userInvite.UserProjectStatus is UserProjectStatus.Active) 
                    //Đã có trong dự án rồi, và đang active
                    throw new Exception(Message.IS_EXIST);

                else if (userInvite is null)
                {
                    //Nếu chưa có trong dự án thì sẽ được thêm vào với trạng thái pending
                    var userProjectAdd = new UserProject()
                    {
                        ProjectId = request.ProjectId,
                        UserId = user.Id,
                        UserProjectStatus = UserProjectStatus.Pending,
                        DateJoined = DateTime.UtcNow,
                    };
                    await userProjectRepository.AddAsync(userProjectAdd, cancellationToken);
                    await userProjectRepository.SaveChangeAsync(cancellationToken);
                }

                /*Có trong dự án nhưng đang trong trạng thái pending 
                thì sẽ chỉ gửi mail và update cái mới*/
                else
                {
                    userInvite.Role = request.Role;
                    userProjectRepository.Update(userInvite);
                    await userProjectRepository.SaveChangeAsync(cancellationToken);
                }



                //Gửi message cho service event
                var invitationUser = new InvitationUserEvent()
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = userProject.Role == Role.Admin ? "Quản trị viên" : "Người dùng",
                    ProjectId = request.ProjectId,
                    UserId = user.Id
                };

                await publishEndpoint.Publish(invitationUser, cancellationToken);

                var activityEvent = new CreateActivityEvent
                {
                    Action = "INVITE_USER",
                    ResourceId = user.Id,
                    Content = $"Đã mời người dùng có email {user.Email} vào dự án với vai trò {request.Role}.",
                    TypeActivity = TypeActivity.Team,
                    ProjectId = request.ProjectId
                };

                await publishEndpoint.Publish(activityEvent, cancellationToken);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                throw new NotFoundException(Message.NOT_FOUND);
            }
            
           
            return new InviteUserResponse() { Data = true, Message = Message.INVITATION_USER };

        }
    }
}
