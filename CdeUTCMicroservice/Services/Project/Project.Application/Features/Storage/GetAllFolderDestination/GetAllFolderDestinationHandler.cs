namespace Project.Application.Features.Storage.GetAllFolderDestination
{
    public class GetAllFolderDestinationHandler
        (IBaseRepository<Folder> folderRepository,
        IBaseRepository<File> fileRepository)
        : IQueryHandler<GetAllFolderDestinationRequest, ApiResponse<List<GetAllFolderDestinationResponse>>>
    {
        public async Task<ApiResponse<List<GetAllFolderDestinationResponse>>> Handle(GetAllFolderDestinationRequest request, CancellationToken cancellationToken)
        {
            var parentIdOfFiles = await fileRepository.GetAllQueryAble()
             .Where(e => request.FileIds.Contains(e.Id))
             .Select(e => e.FolderId!.Value) //Folder id luôn có giá trị
             .ToListAsync(cancellationToken);

            var parentIdOfFolders = await folderRepository.GetAllQueryAble()
                .Where(e => request.FolderIds.Contains(e.Id))
                .Select(e => e.ParentId)
                .ToListAsync(cancellationToken);

            //Check xem là tất cả cùng thuộc một folder không
            var mergeList = parentIdOfFiles.Concat(parentIdOfFolders).Distinct().ToList();

            if (mergeList.Count > 1)  //Tức là nó không cùng 1 cha
                throw new BadRequestException(Message.NOT_SAME_PARENT);

            /*
             * Lấy ra tất cả các các folder có parent id đã cho
             * Và phải khác với nhừng list folder đã trên
             */

            var foldersDestination = await folderRepository.GetAllQueryAble()
                .Where(e => !request.FolderIds.Contains(e.Id) 
                && e.ParentId == request.ParentId)
                .Select(e => new GetAllFolderDestinationResponse()
                {
                    Id = e.Id,
                    Name = e.Name,
                })
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<GetAllFolderDestinationResponse>> { Data = foldersDestination, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
