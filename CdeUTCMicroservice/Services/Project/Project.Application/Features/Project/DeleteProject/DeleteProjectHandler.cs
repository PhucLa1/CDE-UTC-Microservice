﻿namespace Project.Application.Features.Project.DeleteProject
{
    public class DeleteProjectHandler
        (IBaseRepository<ProjectEntity> projectEntityRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<DeleteProjectRequest, DeleteProjectResponse>
    {
        public async Task<DeleteProjectResponse> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.Id && e.UserProjectStatus == UserProjectStatus.Active);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var project = await projectEntityRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (project is null)
                throw new NotFoundException(Message.NOT_FOUND);

            projectEntityRepository.Remove(project);
            await projectEntityRepository.SaveChangeAsync(cancellationToken);
            return new DeleteProjectResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };

        }
    }
}
