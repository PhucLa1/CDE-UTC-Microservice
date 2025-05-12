
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Transports;

namespace Project.Application.Features.Groups.CreateGroup
{
    public class CreateGroupHandler
        (IBaseRepository<Group> groupRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<CreateGroupRequest, CreateGroupResponse>
    {
        public async Task<CreateGroupResponse> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            /*
             Admin được tạo mới group
             Member không được tạo mới
             */
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);
            var group = new Group()
            {
                Name = request.Name,
                ProjectId = request.ProjectId
            };
            await groupRepository.AddAsync(group, cancellationToken);
            await groupRepository.SaveChangeAsync(cancellationToken);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = group.Id,
                Content = "Đã tạo mới nhóm mang tên " + request.Name,
                TypeActivity = TypeActivity.Group,
                ProjectId = request.ProjectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new CreateGroupResponse() { Data = true, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
