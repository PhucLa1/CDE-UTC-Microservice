
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Groups.DeleteUserGroup
{
    public class DeleteUserGroupHandler
        (IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<UserGroup> userGroupRepository,
        IPublishEndpoint publishEndpoint,
        IUserGrpc userGrpc,
        IBaseRepository<Group> groupRepository)
        : ICommandHandler<DeleteUserGroupRequest, DeleteUserGroupResponse>
    {
        public async Task<DeleteUserGroupResponse> Handle(DeleteUserGroupRequest request, CancellationToken cancellationToken)
        {
            /*
            * Admin: được xóa người ra khỏi group
            * Member : không được 
            */
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            //Nếu không phải admin và người xóa cũng không phải là chính mình
            if (userProject.Role is not Role.Admin && request.UserId != userCurrentId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var userGroup = await userGroupRepository.GetAllQueryAble()
                .Where(e => e.UserId == request.UserId && e.GroupId == request.GroupId)
                .FirstOrDefaultAsync(cancellationToken);

            if (userGroup is null)
                throw new NotFoundException(Message.NOT_FOUND);

            userGroupRepository.Remove(userGroup);
            await userGroupRepository.SaveChangeAsync(cancellationToken);

            var group = await groupRepository.GetAllQueryAble().FirstAsync(e => e.Id == userGroup.GroupId);
            var userIds = new List<int>();
            userIds.Add(request.UserId);
            var userEmail = await userGrpc.GetUsersByIds(new GetUserRequestGrpc() { Ids = userIds });
            var userEmailStrings = String.Join(",", userEmail);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = userGroup.Id,
                Content = "Xóa người " + userEmailStrings + " ra khỏi nhóm " + group.Name,
                TypeActivity = TypeActivity.Group,
                ProjectId = request.ProjectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new DeleteUserGroupResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY }; ;
        }
    }
}
