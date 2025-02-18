
namespace Project.Application.Features.Groups.AddUserGroup
{
    public class AddUserGroupHandler
        (IBaseRepository<UserGroup> userGroupRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<AddUserGroupRequest, AddUserGroupResponse>
    {
        public async Task<AddUserGroupResponse> Handle(AddUserGroupRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            //Lấy ra những người dùng không thuộc trong đó
            var userGroupIds = await userGroupRepository.GetAllQueryAble()
                .Where(e => e.GroupId == request.GroupId)
                .Select(e => e.UserId)
                .ToListAsync(cancellationToken);

            var userIds = request.UserIds.Except(userGroupIds).ToList();

            var userGroupAdds = new List<UserGroup>();
            foreach(var userId in userIds)
            {
                var userGroupAdd = new UserGroup()
                {
                    UserId = userId,
                    GroupId = request.GroupId,
                    DateJoined = DateTime.UtcNow,
                };
                userGroupAdds.Add(userGroupAdd);
            }
            
            await userGroupRepository.AddRangeAsync(userGroupAdds, cancellationToken);
            await userGroupRepository.SaveChangeAsync(cancellationToken);

            return new AddUserGroupResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
