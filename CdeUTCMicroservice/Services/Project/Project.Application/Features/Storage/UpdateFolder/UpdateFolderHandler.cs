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
                folder.Name = request.Name; //Sửa tên
                folder.Version = folder.Version + 1; //Tăng lên một version
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
