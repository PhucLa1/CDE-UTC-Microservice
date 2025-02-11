
namespace Project.Application.Features.Permission.ChangePermission
{
    public class ChangePermissionHandler
        (IBaseRepository<ProjectEntity> projectEntityRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<ChangePermissionRequest, ChangePermissionResponse>
    {
        public async Task<ChangePermissionResponse> Handle(ChangePermissionRequest request, CancellationToken cancellationToken)
        {
            //Chỉ admin mới có quyền thay đổi
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var project = await projectEntityRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.ProjectId);

            if(project is null)
                throw new NotFoundException(Message.NOT_FOUND);

            project.TodoVisibility = request.TodoVisibility;
            project.InvitationPermission = request.InvitationPermission;
            projectEntityRepository.Update(project);
            await projectEntityRepository.SaveChangeAsync(cancellationToken);

            return new ChangePermissionResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };

        }
    }
}
