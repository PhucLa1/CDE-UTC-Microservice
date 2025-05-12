
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Storage.MoveFile
{
    public class MoveFileHandler
        (IBaseRepository<File> fileRepository,
        IBaseRepository<Folder> folderRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<MoveFileRequest, MoveFileResponse>
    {
        public async Task<MoveFileResponse> Handle(MoveFileRequest request, CancellationToken cancellationToken)
        {
            var fullPath = "";
            //Lấy ra folder đó
            var folderFullPath = await folderRepository.GetAllQueryAble()
                .Where(e => e.Id == request.FolderId)
                .Select(e => e.FullPath)
                .FirstOrDefaultAsync(cancellationToken);

            var file = await fileRepository.GetAllQueryAble()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var folder = await folderRepository.GetAllQueryAble()
                .Where(e => e.Id == request.FolderId)
                .FirstOrDefaultAsync(cancellationToken);


            if (file is null)
                throw new NotFoundException(Message.NOT_FOUND);
            if (file.FolderId == request.FolderId)
                return new MoveFileResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
            //Update file
            if (folderFullPath is not null)
                fullPath = folderFullPath + "/" + file.Id;

            file.FullPath = fullPath;
            file.FolderId = request.FolderId;

            fileRepository.Update(file);
            await fileRepository.SaveChangeAsync(cancellationToken);

            var eventMessage = new CreateActivityEvent
            {
                Action = "MOVE",
                ResourceId = file.Id,
                Content = $"Đã di chuyển tập tin \"{file.Name}\" sang thư mục " + folder.Name,
                TypeActivity = TypeActivity.File,
                ProjectId = file.ProjectId.Value,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);


            return new MoveFileResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
