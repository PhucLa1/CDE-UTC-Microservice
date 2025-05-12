using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Unit.UpdateUnitByProjectId
{
    public class UpdateUnitByProjectIdHandler
        (IBaseRepository<ProjectEntity> projectEntityRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateUnitByProjectIdRequest, UpdateUnitByProjectIdResponse>
    {
        public async Task<UpdateUnitByProjectIdResponse> Handle(UpdateUnitByProjectIdRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var project = await projectEntityRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.ProjectId);

            if (project is null)
                throw new NotFoundException(Message.NOT_FOUND);

            //Sửa các thuộc tính
            project.UnitSystem = request.UnitSystem;
            project.UnitLength = request.UnitLength;
            project.UnitLengthPrecision = request.UnitLengthPrecision;
            project.IsCheckMeasurement = request.IsCheckMeasurement;
            project.UnitArea = request.UnitArea;
            project.UnitAreaPrecision = request.UnitAreaPrecision;
            project.UnitWeight = request.UnitWeight;
            project.UnitWeightPrecision = request.UnitWeightPrecision;
            project.UnitVolume = request.UnitVolume;
            project.UnitVolumePrecision = request.UnitVolumePrecision;
            project.UnitAngle = request.UnitAngle;
            project.UnitAnglePrecision = request.UnitAnglePrecision;

            //Save
            projectEntityRepository.Update(project);
            await projectEntityRepository.SaveChangeAsync(cancellationToken);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = project.Id,
                Content = "Đã cập nhật phần độ đo của dự án ",
                TypeActivity = TypeActivity.Project,
                ProjectId = project.Id,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new UpdateUnitByProjectIdResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
