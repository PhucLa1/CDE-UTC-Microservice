
namespace Project.Application.Features.Team.KickUserFromProject
{
    public class KickUserFromProjectHandler
        (IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<KickUserFromProjectRequest, KickUserFromProjectResponse>
    {
        public async Task<KickUserFromProjectResponse> Handle(KickUserFromProjectRequest request, CancellationToken cancellationToken)
        {
            //Nếu người xóa không phải là admin
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var userProjectDelete = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == request.UserId && e.ProjectId == request.ProjectId);

            if (userProjectDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            userProjectRepository.Remove(userProjectDelete);
            await userProjectRepository.SaveChangeAsync(cancellationToken);
            return new KickUserFromProjectResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
