using Project.Domain.Extensions;

namespace Project.Application.Features.Storage.CreateFile;

public class CreateFileHandler
    (IBaseRepository<File> fileRepository,
    IBaseRepository<FileHistory> fileHistoryRepository)
    : ICommandHandler<CreateFileRequest, CreateFileResponse>
{
    public async Task<CreateFileResponse> Handle(CreateFileRequest request, CancellationToken cancellationToken)
    {
        /*
         * Nếu có file đó rồi thì chỉ cập nhật thôi + Thêm 1 bản ghi vào file history
         * Chưa có thì thêm như bình thường
         */
        const decimal fileSizeInMB = 1024 * 1024;
        using var transaction = await fileRepository.BeginTransactionAsync(cancellationToken);

        var fileInDb = await fileRepository.GetAllQueryAble()
            .FirstOrDefaultAsync(e => e.Name == request.Name, cancellationToken);
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
                FileVersion = 0
            };
            await fileRepository.AddAsync(file, cancellationToken);
            await fileRepository.SaveChangeAsync(cancellationToken);
        }
        else
        {
            //Tạo mới bản ghi về file history
            var fileHistory = new FileHistory()
            {
                FileId = fileInDb.Id,
                Name = fileInDb.Name,
                Size = fileInDb.Size,
                Url = fileInDb.Url,
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
        }

        await fileRepository.CommitTransactionAsync(transaction, cancellationToken);

        return new CreateFileResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
    }
}
