
namespace Project.Application.Features.Storage.MoveFolder
{
    public class MoveFolderHandler
        (IBaseRepository<Folder> folderRepository,
        IBaseRepository<File> fileRepository)
        : ICommandHandler<MoveFolderRequest, MoveFolderResponse>
    {
        public async Task<MoveFolderResponse> Handle(MoveFolderRequest request, CancellationToken cancellationToken)
        {
            //Check xem là parent id có phải thuộc trong các folder con của folder cần di chuyển không

            var folder = await folderRepository.GetAllQueryAble()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (folder is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if(folder.ParentId == request.ParentId)
                return new MoveFolderResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };

            var folderIdString = "";

            //Lấy ra những phần tử con
            var childFolderFullPath = await folderRepository.GetAllQueryAble()
                .Where(e => e.FullPath.Contains(folder.FullPath + "/") //Tránh trường hợp là 1/2/3 có thể trùng với 1/2/31
                && e.ParentId != folder.ParentId) //Nhừng folder con là những folder không có cùng cha với thằng folder đang xét
                .Select(e => e.FullPath)
                .OrderByDescending(e => e.Length) // Sắp xếp theo độ dài giảm dần
                .FirstOrDefaultAsync(cancellationToken);

            if (childFolderFullPath is not null)
                folderIdString = childFolderFullPath;

            var folderIdStringArr = folderIdString.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var indexOfFolderId = Array.IndexOf(folderIdStringArr, folder.Id.ToString());
            var folderIds = folderIdStringArr.Skip(indexOfFolderId + 1).Select(int.Parse).ToList(); //Lấy ra những phần tử con của folder
                                                                                                    //
            //Check xem là parent id đó có trong folder con không ?
            if (folderIds.Contains(request.ParentId))
                throw new BadRequestException(Message.CAN_NOT_MOVE_FOLDER);

            var fullPath = "";
            var fullPathName = "";
            var folderParent = await folderRepository.GetAllQueryAble()
                .Where(e => e.Id == request.ParentId)
                .Select(e => new
                {
                    FullPath = e.FullPath,
                    FullPathName = e.FullPathName
                }).FirstOrDefaultAsync(cancellationToken);

            if (folderParent is not null)
            {
                fullPath = folderParent.FullPath;
                fullPathName = folderParent.FullPathName;
            } //Tức là parent Id != 0


            //Lấy ra những thằng folder con của nó và nó
            var folderUpdates = await folderRepository.GetAllQueryAble()
                .Where(e => folderIds.Contains(e.Id) || e.Id == request.Id)
                .OrderBy(e => e.FullPath) //Sắp xếp từ folder con gần nhất
                .ToListAsync(cancellationToken);

            var fileUpdates = await fileRepository.GetAllQueryAble()
                .Where(e => e.FullPath.Contains(folder.FullPath + "/"))
                .OrderBy(e => e.FullPath) //Sắp xếp từ file con gần nhất
                .ToListAsync(cancellationToken);


            foreach (var folderUpdate in folderUpdates)
            {
                fullPath = fullPath + "/" + folderUpdate.Id;
                fullPathName = fullPathName + "/" + folderUpdate.Name;

                //Cập nhật
                folderUpdate.FullPath = fullPath;
                folderUpdate.FullPathName = fullPathName;
            }
            foreach (var fileUpdate in fileUpdates)
            {
                //Cập nhật
                var pathFile = folderUpdates
                    .Where(e => e.Id == fileUpdate.FolderId)
                    .Select(e => e.FullPath)
                    .First();
                fileUpdate.FullPath = pathFile + "/" + fileUpdate.Id;
            }

            //Cập nhật parent id
            folder.ParentId = request.ParentId;

            folderRepository.UpdateMany(folderUpdates);
            fileRepository.UpdateMany(fileUpdates);
            await folderRepository.SaveChangeAsync(cancellationToken);

            return new MoveFolderResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
