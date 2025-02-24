
using Project.Application.Features.Storage.MoveFolder;
using Project.Domain.Entities;

namespace Project.Application.Features.Storage.MoveFile
{
    public class MoveFileHandler
        (IBaseRepository<File> fileRepository,
        IBaseRepository<Folder> folderRepository)
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

            return new MoveFileResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
