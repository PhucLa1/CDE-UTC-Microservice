using Project.Application.Extensions;

namespace Project.Application.Features.Storage.UpdateFile
{
    public class UpdateFileHandler
        (IBaseRepository<File> fileRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<FileTag> fileTagRepository,
        IBaseRepository<FileHistory> fileHistoryRepository)
        : ICommandHandler<UpdateFileRequest, UpdateFileResponse>
    {
        /*
         * Update : 
         * Bao gồm việc sửa, thêm tag và việc sửa tên
         * Quyền : 
         * Admin : Toàn quyền sửa
         * Member : Chỉ được sửa những file mà mình đã tạo ra
         */
        public async Task<UpdateFileResponse> Handle(UpdateFileRequest request, CancellationToken cancellationToken)
        {
            var IMAGE_EXTENSION = new List<string>() { ".png", ".jpg", ".jpeg" };
            var currentUserId = userProjectRepository.GetCurrentId();

            var userProject = await userProjectRepository.GetAllQueryAble()
               .FirstOrDefaultAsync(e => e.UserId == currentUserId && e.ProjectId == request.ProjectId);

            var file = await fileRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || file is null)
                throw new NotFoundException(Message.NOT_FOUND);


            //Không phải admin và cũng không phải người tạo tệp
            if (userProject.Role is not Role.Admin && file.CreatedBy != currentUserId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            //Xóa hết fileTag đi
            var fileTags = await fileTagRepository.GetAllQueryAble()
                .Where(e => e.FileId == request.Id)
                .ToListAsync(cancellationToken);

            //Sửa tên của file. Nếu tên khác tên cũ thì update lên 1 ver mới 
            if (file.Name != request.Name)
            {
                //Tạo mới một bản ghi trong file history
                var fileHistory = new FileHistory()
                {
                    Name = file.Name,
                    FileVersion = file.FileVersion,
                    FileId = request.Id,
                    Url = IMAGE_EXTENSION.Contains(file.Extension)
                    ? file.Url
                    : file.Extension.ConvertToUrl(),
                    Extension = file.Extension
                };
                await fileHistoryRepository.AddAsync(fileHistory, cancellationToken);
                file.Name = request.Name; //Sửa tên
                file.FileVersion = file.FileVersion + 1; //Tăng lên một version
            }

            fileRepository.Update(file);

            var existingTags = await fileTagRepository.GetAllQueryAble()
                .Where(e => e.FileId == request.Id)
                .ToListAsync(cancellationToken);

            // Tag hiện tại
            var existingTagIds = existingTags.Select(e => e.TagId!.Value).ToHashSet();

            // Tag mới từ request
            var newTagIds = request.TagIds.ToHashSet();

            // Tìm tag cần thêm(Lấy những id không thuộc trong những tag id mới)
            var tagsToAdd = newTagIds.Except(existingTagIds)
                .Select(tagId => new FileTag { TagId = tagId, FileId = request.Id })
                .ToList();
            // Tìm tag cần xóa
            var tagsToRemove = existingTags.Where(e => !newTagIds.Contains(e.TagId!.Value)).ToList();

            // Xóa và thêm chỉ khi cần thiết
            if (tagsToRemove.Any())
                fileTagRepository.RemoveRange(tagsToRemove);
            if (tagsToAdd.Any())
                await fileTagRepository.AddRangeAsync(tagsToAdd, cancellationToken);

            await fileTagRepository.SaveChangeAsync(cancellationToken);

            return new UpdateFileResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
