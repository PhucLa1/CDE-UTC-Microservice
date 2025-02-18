
namespace Project.Application.Features.Groups.DeleteUserGroup
{
    public class DeleteUserGroupHandler
        (IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<UserGroup> userGroupRepository)
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

            return new DeleteUserGroupResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY }; ;
        }
    }
}
