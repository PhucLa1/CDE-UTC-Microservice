﻿

using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Project.CreateProject
{
    public class CreateProjectHandler
        (IBaseRepository<ProjectEntity> projectEntityRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<CreateProjectRequest, CreateProjectResponse>
    {
        public async Task<CreateProjectResponse> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            using var transaction = await projectEntityRepository.BeginTransactionAsync(cancellationToken);
            var project = new ProjectEntity()
            {
                Name = request.Name,
                ImageUrl = HandleFile.UPLOAD("Project", request.Image),
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            await projectEntityRepository.AddAsync(project, cancellationToken);
            await projectEntityRepository.SaveChangeAsync(cancellationToken);
            var userProject = new UserProject()
            {
                UserId = userProjectRepository.GetCurrentId(),
                ProjectId = project.Id,
                Role = Role.Admin,
                UserProjectStatus = UserProjectStatus.Active,
                LastAccessed = DateTime.UtcNow,
                DateJoined = DateTime.UtcNow
            };           
            await userProjectRepository.AddAsync(userProject, cancellationToken);
            await projectEntityRepository.SaveChangeAsync(cancellationToken);
            await projectEntityRepository.CommitTransactionAsync(transaction, cancellationToken);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = project.Id,
                Content = "Đã tạo mới dự án tên "+ project.Name,
                TypeActivity = TypeActivity.Project,
                ProjectId = project.Id,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            var eventCreateProject = new CreateProjectEvent()
            {
                ProjectId = project.Id,
            };
            await publishEndpoint.Publish(eventCreateProject, cancellationToken);

            return new CreateProjectResponse() { Data = true,  Message= Message.CREATE_SUCCESSFULLY };
        }
    }
}
