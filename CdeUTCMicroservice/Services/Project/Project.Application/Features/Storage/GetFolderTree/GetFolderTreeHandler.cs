
using Project.Application.Dtos.Result;

namespace Project.Application.Features.Storage.GetFolderTree
{
    public class GetFolderTreeHandler
        (IBaseRepository<Folder> folderRepository,
        IBaseRepository<File> fileRepository)
        : IQueryHandler<GetFolderTreeRequest, ApiResponse<GetFolderTreeResponse>>
    {
        public async Task<ApiResponse<GetFolderTreeResponse>> Handle(GetFolderTreeRequest request, CancellationToken cancellationToken)
        {
            var folder = await folderRepository.GetAllQueryAble()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);    

            if(folder is null)
                throw new NotFoundException(Message.NOT_FOUND); 


            var childFolder = await folderRepository.GetAllQueryAble()
                .Where(e => e.FullPath.Contains(folder.FullPath + "/"))
                .ToListAsync(cancellationToken);

            var childFile = await fileRepository.GetAllQueryAble()
                .Where(e => e.FullPath.Contains(folder.FullPath + "/"))
                .ToListAsync(cancellationToken);

            var result = BuildFolderTree(folder, childFolder, childFile);

            return new ApiResponse<GetFolderTreeResponse>() { Data = result, Message = Message.GET_SUCCESSFULLY };

            
        }

        private GetFolderTreeResponse BuildFolderTree(Folder folder, List<Folder> allFolders, List<File> allFiles)
        {
            var treeFolder = new GetFolderTreeResponse
            {
                Name = folder.Name,
                Files = allFiles
                    .Where(file => file.FullPath.StartsWith(folder.FullPath + "/") && !file.FullPath.Substring(folder.FullPath.Length + 1).Contains("/"))
                    .Select(file => new FileResult
                    { 

                        Name = file.Name + (file.Extension == ".extension" ? "" : file.Extension),
                        Url = file.Url 
                    })
                    .ToList(),
                SubFolders = allFolders
                    .Where(subFolder => subFolder.FullPath.StartsWith(folder.FullPath + "/") && !subFolder.FullPath.Substring(folder.FullPath.Length + 1).Contains("/"))
                    .Select(subFolder => BuildFolderTree(subFolder, allFolders, allFiles))
                    .ToList()
            };

            return treeFolder;
        }
    }
}
