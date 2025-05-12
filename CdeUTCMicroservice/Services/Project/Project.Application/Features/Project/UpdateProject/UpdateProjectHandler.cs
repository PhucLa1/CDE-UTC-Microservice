
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Project.UpdateProject
{
    public class UpdateProjectHandler
        (IBaseRepository<ProjectEntity> projectEntityRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateProjectRequest, UpdateProjectResponse>
    {
        public async Task<UpdateProjectResponse> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.Id && e.UserProjectStatus == UserProjectStatus.Active);

            if(userProject is null) 
                throw new NotFoundException(Message.NOT_FOUND);

            if(userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);


            var project = await projectEntityRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (project is null)
                throw new NotFoundException(Message.NOT_FOUND);

            project.Name = request.Name;
            project.Description = request.Description;
            project.StartDate = request.StartDate;
            project.EndDate = request.EndDate;
            if(request.Image.Length > 0)
                project.ImageUrl = HandleFile.UPLOAD("Project", request.Image);

            projectEntityRepository.Update(project);
            await projectEntityRepository.SaveChangeAsync(cancellationToken);

            //Gửi message 
            var eventMessage = new CreateActivityEvent()
            {
                Action = "UPDATE",
                ResourceId = project.Id,
                Content = "Chỉnh sửa thông tin dự án " + project.Name,
                TypeActivity = TypeActivity.Project,
                ProjectId = projectEntityRepository.GetProjectId(),
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);
            return new UpdateProjectResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
