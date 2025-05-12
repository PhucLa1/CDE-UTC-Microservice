using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Transports;

namespace Project.Application.Features.Groups.DeleteGroup
{
    public class DeleteGroupHandler
         (IBaseRepository<Group> groupRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<DeleteGroupRequest, DeleteGroupResponse>
    {
        public async Task<DeleteGroupResponse> Handle(DeleteGroupRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được xóa group
            * Member : Không được xóa
            */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var group = await groupRepository.GetAllQueryAble()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (group is null)
                throw new NotFoundException(Message.NOT_FOUND);

            groupRepository.Remove(group);
            await groupRepository.SaveChangeAsync(cancellationToken);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = group.Id,
                Content = "Đã xóa nhóm mang tên " + group.Name,
                TypeActivity = TypeActivity.Group,
                ProjectId = request.ProjectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new DeleteGroupResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
