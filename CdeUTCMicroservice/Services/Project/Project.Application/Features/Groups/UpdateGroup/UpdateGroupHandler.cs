
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Transports;

namespace Project.Application.Features.Groups.UpdateGroup
{
    public class UpdateGroupHandler
        (IBaseRepository<Group> groupRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateGroupRequest, UpdateGroupResponse>
    {
        public async Task<UpdateGroupResponse> Handle(UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            /*
             * Admin: được sửa group
             * Member : không được 
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

            var oldName = group.Name;
            group.Name = request.Name;

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = group.Id,
                Content = "Đổi mới tên nhóm từ " + oldName + " sang tên " + request.Name,
                TypeActivity = TypeActivity.Group,
                ProjectId = request.ProjectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            groupRepository.Update(group);
            await groupRepository.SaveChangeAsync(cancellationToken);
            return new UpdateGroupResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
