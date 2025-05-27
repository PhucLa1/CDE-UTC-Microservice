using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Storage.DeleteFile
{
    internal class DeleteFileHandler
        (IBaseRepository<File> fileRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<DeleteFileRequest, DeleteFileResponse>
    {
        public async Task<DeleteFileResponse> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = userProjectRepository.GetCurrentId();

            var userProject = await userProjectRepository.GetAllQueryAble()
               .FirstOrDefaultAsync(e => e.UserId == currentUserId && e.ProjectId == request.ProjectId);

            var file = await fileRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || file is null)
                throw new NotFoundException(Message.NOT_FOUND);


            //Không phải admin và cũng không phải người tạo thư mục
            if (userProject.Role is not Role.Admin && file.CreatedBy != currentUserId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            fileRepository.Remove(file);
            await fileRepository.SaveChangeAsync(cancellationToken);

            // Gửi message activity
            var eventMessage = new CreateActivityEvent
            {
                Action = "DELETE",
                ResourceId = file.Id,
                Content = $"Đã xóa tệp \"{file.Name}\"",
                TypeActivity = TypeActivity.File,
                ProjectId = request.ProjectId
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new DeleteFileResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
