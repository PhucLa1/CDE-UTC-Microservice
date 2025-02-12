
namespace Project.Application.Features.Groups.UpdateGroup
{
    public class UpdateGroupHandler
        (IBaseRepository<Group> groupRepository,
        IBaseRepository<UserProject> userProjectRepository)
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

            group.Name = request.Name;
            groupRepository.Update(group);
            await groupRepository.SaveChangeAsync(cancellationToken);
            return new UpdateGroupResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
