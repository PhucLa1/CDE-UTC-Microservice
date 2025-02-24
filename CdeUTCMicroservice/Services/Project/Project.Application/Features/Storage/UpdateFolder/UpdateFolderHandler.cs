namespace Project.Application.Features.Storage.UpdateFolder
{
    public class UpdateFolderHandler
        (IBaseRepository<Folder> folderRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<FolderTag> folderTagRepository,
        IBaseRepository<FolderHistory> folderHistoryRepository)
        : ICommandHandler<UpdateFolderRequest, UpdateFolderResponse>
    {
        /*
         * Update : 
         * Bao gồm việc sửa, thêm tag và việc sửa tên
         * Quyền : 
         * Admin : Toàn quyền sửa
         * Member : Chỉ được sửa những folder mà mình đã tạo ra
         */
        public async Task<UpdateFolderResponse> Handle(UpdateFolderRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = userProjectRepository.GetCurrentId();

            var userProject = await userProjectRepository.GetAllQueryAble()
               .FirstOrDefaultAsync(e => e.UserId == currentUserId && e.ProjectId == request.ProjectId);

            var folder = await folderRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || folder is null)
                throw new NotFoundException(Message.NOT_FOUND);


            //Không phải admin và cũng không phải người tạo thư mục
            if (userProject.Role is not Role.Admin && folder.CreatedBy != currentUserId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            //Xóa hết folderTag đi
            var folderTags = await folderTagRepository.GetAllQueryAble()
                .Where(e => e.FolderId == request.Id)
                .ToListAsync(cancellationToken);

            //Sửa tên của folder. Nếu tên khác tên cũ thì update lên 1 ver mới 
            if(folder.Name != request.Name)
            {
                //Tạo mới một bản ghi trong folder history
                var folderHistory = new FolderHistory()
                {
                    Name = folder.Name,
                    Version = folder.Version,
                    FolderId = request.Id,
                };
                await folderHistoryRepository.AddAsync(folderHistory, cancellationToken);
                
                folder.Version = folder.Version + 1; //Tăng lên một version

                #region Cập nhật fullpathname
                var folderNameString = "";
                var folderIdString = "";
                var childFolder = await folderRepository.GetAllQueryAble()
                    .Where(e => e.FullPath.Contains(folder.FullPath + "/"))
                    .OrderByDescending(e => e.FullPath)
                    .Select(e => new
                    {
                        FullPathName = e.FullPathName,
                        FullPath = e.FullPath
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (childFolder is not null)
                {
                    folderNameString = childFolder.FullPathName;
                    folderIdString = childFolder.FullPath;
                }


                //Thay phần folder full path name
                var folderIdStringArr = folderIdString.Split('/');
                var indexOfFolderId = Array.IndexOf(folderIdStringArr, folder.Id.ToString());
                var folderNameStringArr = folderNameString.Split('/');
                folderNameStringArr[indexOfFolderId] = request.Name;
                folder.FullPathName = string.Join("/", folderNameStringArr);
                var fullPath = string.Join("/", folderNameStringArr);
                //Lấy ra những folder id con
                var folderIds = folderIdStringArr.Skip(indexOfFolderId + 1).Select(int.Parse).ToList(); //Lấy ra những phần tử con của folder
                var folders = await folderRepository.GetAllQueryAble()
                    .Where(e => folderIds.Contains(e.Id))
                    .OrderBy(e => e.FullPathName)
                    .ToListAsync(cancellationToken);

                foreach(var folderUpdate in folders)
                {
                    fullPath = fullPath + "/" + folderUpdate.Name;
                    folderUpdate.FullPathName = fullPath;
                }

                folderRepository.UpdateMany(folders);
                #endregion
                folder.Name = request.Name; //Sửa tên
            }
            
            folderRepository.Update(folder);
            

            var existingTags = await folderTagRepository.GetAllQueryAble()
                .Where(e => e.FolderId == request.Id)
                .ToListAsync(cancellationToken);

            // Tag hiện tại
            var existingTagIds = existingTags.Select(e => e.TagId!.Value).ToHashSet();

            // Tag mới từ request
            var newTagIds = request.TagIds.ToHashSet();

            // Tìm tag cần thêm(Lấy những id không thuộc trong những tag id mới)
            var tagsToAdd = newTagIds.Except(existingTagIds)
                .Select(tagId => new FolderTag { TagId = tagId, FolderId = request.Id })
                .ToList();
            // Tìm tag cần xóa
            var tagsToRemove = existingTags.Where(e => !newTagIds.Contains(e.TagId!.Value)).ToList();

            // Xóa và thêm chỉ khi cần thiết
            if (tagsToRemove.Any())
                folderTagRepository.RemoveRange(tagsToRemove);
            if (tagsToAdd.Any())
                await folderTagRepository.AddRangeAsync(tagsToAdd, cancellationToken);

            await folderTagRepository.SaveChangeAsync(cancellationToken);

            return new UpdateFolderResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
