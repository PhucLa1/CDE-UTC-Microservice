

namespace Project.Application.Features.Project.CreateProject
{
    public class CreateProjectHandler
        (IBaseRepository<ProjectEntity> projectEntityRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<CreateProjectRequest, CreateProjectResponse>
    {
        public async Task<CreateProjectResponse> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var project = new ProjectEntity()
            {
                Id = ProjectId.Of(Guid.NewGuid()),
                Name = request.Name,
                ImageUrl = HandleFile.UPLOAD("Project", request.Image),
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            var userProject = new UserProject()
            {
                Id = UserProjectId.Of(Guid.NewGuid()),
                UserId = userProjectRepository.GetCurrentId(),
                ProjectId = project.Id,
                Role = Role.Admin,
                UserProjectStatus = UserProjectStatus.Active,
                LastAccessed = DateTime.UtcNow,
                DateJoined = DateTime.UtcNow
            };
            await projectEntityRepository.AddAsync(project, cancellationToken);
            await userProjectRepository.AddAsync(userProject, cancellationToken);
            await projectEntityRepository.SaveChangeAsync(cancellationToken);
            return new CreateProjectResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
