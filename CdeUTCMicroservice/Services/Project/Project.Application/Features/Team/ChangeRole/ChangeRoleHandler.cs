
namespace Project.Application.Features.Team.ChangeRole
{
    public class ChangeRoleHandler
        (IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<ChangeRoleRequest, ChangeRoleResponse>
    {
        public async Task<ChangeRoleResponse> Handle(ChangeRoleRequest request, CancellationToken cancellationToken)
        {
            /*
             * Chỉ admin mới có quyền thay đôi role
             * Dự án phải có ít nhất 1 admin
             */
            //Chỉ admin mới có quyền thay đổi
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var countAdmin = await userProjectRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == request.ProjectId && e.Role == Role.Admin)
                .CountAsync(cancellationToken);

            var userProjectUpdate = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == request.UserId && e.ProjectId == request.ProjectId);

            if (userProjectUpdate is null)
                throw new NotFoundException(Message.NOT_FOUND);

            //Nếu chuyển role thành member trong 1 dự án mà chỉ còn 1 admin
            //Người được update cũng là người admin cuối cùng thì không được
            if (request.Role is Role.Member 
                && countAdmin == 1 && userProjectUpdate.Role == Role.Admin)
                throw new Exception(Message.MUST_HAVE_ONE_ADMIN);



            userProjectUpdate.Role = request.Role;
            userProjectRepository.Update(userProjectUpdate);
            await userProjectRepository.SaveChangeAsync(cancellationToken);

            return new ChangeRoleResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };



        }
    }
}
