using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Transports;
using Project.Application.Extensions;
using Project.Domain.Extensions;

namespace Project.Application.Features.Storage.CreateFile;

public class CreateFileHandler
    (IBaseRepository<File> fileRepository,
    IBaseRepository<FileHistory> fileHistoryRepository,
    IBaseRepository<Folder> folderRepository,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateFileRequest, CreateFileResponse>
{
    public async Task<CreateFileResponse> Handle(CreateFileRequest request, CancellationToken cancellationToken)
    {
        /*
         * Nếu có file đó rồi thì chỉ cập nhật thôi + Thêm 1 bản ghi vào file history
         * Chưa có thì thêm như bình thường
         */
        var IMAGE_EXTENSION = new List<string>() { ".png", ".jpg", ".jpeg" };
        const decimal fileSizeInMB = 1024 * 1024;
        using var transaction = await fileRepository.BeginTransactionAsync(cancellationToken);
        CreateActivityEvent eventMessage;
        string fullPath = "";
        if(request.FolderId is not 0)
        {
            var parent = await folderRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.FolderId);

            if (parent is null)
                throw new NotFoundException(Message.NOT_FOUND);

            fullPath = parent.FullPath;
        }

        //Kiểm tra xem tên đó có ở trong thư mục và cùng dự án không
        var fileInDb = await fileRepository.GetAllQueryAble()
            .FirstOrDefaultAsync(e => e.Name == request.Name 
            && e.FolderId == request.FolderId, cancellationToken);
        if(fileInDb is null)
        {
            var file = new File
            {
                Name = request.Name,
                Size = request.Size / fileSizeInMB,
                Url = request.Url,
                FolderId = request.FolderId,
                ProjectId = request.ProjectId,
                FileType = IconFileExtension.GetFileType(request.Name),
                Extension = request.Extension,
                MimeType = request.MimeType,
                FileVersion = 0,
            };
            await fileRepository.AddAsync(file, cancellationToken);
            await fileRepository.SaveChangeAsync(cancellationToken);

            file.FullPath = fullPath + "/" + file.Id;
            fileRepository.Update(file);
            await fileRepository.SaveChangeAsync(cancellationToken);

            eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = file.Id,
                Content = $"Đã tạo mới tệp \"{file.Name}\"",
                TypeActivity = TypeActivity.Project,
                ProjectId = request.ProjectId.Value,
            };
        }
        else
        {
            //Tạo mới bản ghi về file history
            var fileHistory = new FileHistory()
            {
                FileId = fileInDb.Id,
                Name = fileInDb.Name,
                Size = fileInDb.Size,
                Url = IMAGE_EXTENSION.Contains(fileInDb.Extension)
                    ? fileInDb.Url
                    : fileInDb.Extension.ConvertToUrl(),
                FileVersion = fileInDb.FileVersion,
                FileType = fileInDb.FileType,
                MimeType= fileInDb.MimeType,
                Extension = fileInDb.Extension
            };
            await fileHistoryRepository.AddAsync(fileHistory, cancellationToken);

            //Cập nhật thông tin
            fileInDb.Name = request.Name;
            fileInDb.Size = request.Size / fileSizeInMB;
            fileInDb.Url = request.Url;
            fileInDb.FolderId = request.FolderId;
            fileInDb.ProjectId = request.ProjectId;
            fileInDb.FileType = IconFileExtension.GetFileType(request.Name);
            fileInDb.Extension = request.Extension;
            fileInDb.MimeType = request.MimeType;
            fileInDb.FileVersion = fileInDb.FileVersion + 1;

            fileRepository.Update(fileInDb);
            await fileHistoryRepository.SaveChangeAsync(cancellationToken);

            eventMessage = new CreateActivityEvent()
            {
                Action = "UPDATE",
                ResourceId = fileInDb.Id,
                Content = $"Đã cập nhật phiên bản mới cho tệp \"{fileInDb.Name}\" (v{fileInDb.FileVersion})",
                TypeActivity = TypeActivity.File,
                ProjectId = request.ProjectId.Value,
            };
        }

        await fileRepository.CommitTransactionAsync(transaction, cancellationToken);

        //Gửi message sang bên event
        
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        return new CreateFileResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
    }
}
