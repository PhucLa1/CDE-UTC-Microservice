
namespace Project.Application.Features.Storage.DeleteStorages
{
    public class DeleteStoragesHandler
        (IBaseRepository<File> fileRepository,
        IBaseRepository<Folder> folderRepository)
        : ICommandHandler<DeleteStoragesRequest, DeleteStoragesResponse>
    {
        public async Task<DeleteStoragesResponse> Handle(DeleteStoragesRequest request, CancellationToken cancellationToken)
        {
            var storageModels = request.StorageModels;
            if(storageModels != null)
            {
                var fileIds = storageModels.Where(e => e.IsFile).Select(e => e.Id).ToList();
                var files = await fileRepository.GetAllQueryAble().Where(e => fileIds.Contains(e.Id)).ToListAsync();
                var folderIds = storageModels.Where(e => e.IsFile = false).Select(e => e.Id).ToList();
                var folders = await folderRepository.GetAllQueryAble().Where(e => folderIds.Contains(e.Id)).ToListAsync();
                fileRepository.RemoveRange(files);
                folderRepository.RemoveRange(folders);
                await fileRepository.SaveChangeAsync(cancellationToken);
            }

            return new DeleteStoragesResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
            
        }
    }
}
