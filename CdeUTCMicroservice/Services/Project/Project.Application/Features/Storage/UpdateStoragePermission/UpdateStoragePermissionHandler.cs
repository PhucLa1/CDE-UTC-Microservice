
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Storage.UpdateStoragePermission
{
    public class UpdateStoragePermissionHandler
        (IBaseRepository<File> fileRepository,
        IBaseRepository<Folder> folderRepository,
        IBaseRepository<FilePermission> filePermissionRepository,
        IBaseRepository<FolderPermission> folderPermissionRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateStoragePermissionRequest, UpdateStoragePermissionResponse>
    {
        public async Task<UpdateStoragePermissionResponse> Handle(UpdateStoragePermissionRequest request, CancellationToken cancellationToken)
        {
            if (request.IsFile)
            {
                var file = await fileRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.Id, cancellationToken);
                file.Access = request.Access;

                // Xoá hết quyền cũ
                var oldPermissions = await filePermissionRepository.GetAllQueryAble()
                    .Where(p => p.FileId == file.Id)
                    .ToListAsync(cancellationToken);

                filePermissionRepository.RemoveRange(oldPermissions);

                // Thêm quyền mới
                var newPermissions = request.TargetPermission.Select(p => new FilePermission
                {
                    FileId = file.Id,
                    TargetId = p.Key,
                    Access = p.Value,
                    IsGroup = false,
                });

                await filePermissionRepository.AddRangeAsync(newPermissions, cancellationToken);

                var eventMessage = new CreateActivityEvent
                {
                    Action = "UPDATE",
                    ResourceId = request.Id,
                    Content = "Tệp tin " + file.Name + " được cập nhật quyền thành công",
                    TypeActivity = TypeActivity.File,
                    ProjectId = file.ProjectId.Value,
                };
                await publishEndpoint.Publish(eventMessage, cancellationToken);

            }
            else
            {
                var folder = await folderRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.Id, cancellationToken);
                folder.Access = request.Access;

                // Xoá hết quyền cũ
                var oldPermissions = await folderPermissionRepository.GetAllQueryAble()
                    .Where(p => p.FolderId == folder.Id)
                    .ToListAsync(cancellationToken);

                folderPermissionRepository.RemoveRange(oldPermissions);

                // Thêm quyền mới
                var newPermissions = request.TargetPermission.Select(p => new FolderPermission
                {
                    FolderId = folder.Id,
                    TargetId = p.Key,
                    Access = p.Value,
                    IsGroup = false,
                });

                await folderPermissionRepository.AddRangeAsync(newPermissions, cancellationToken);

                var eventMessage = new CreateActivityEvent
                {
                    Action = "UPDATE",
                    ResourceId = request.Id,
                    Content = "Thư mục " + folder.Name + " được cập nhật quyền thành công",
                    TypeActivity = TypeActivity.Folder,
                    ProjectId = folder.ProjectId.Value,
                };
                await publishEndpoint.Publish(eventMessage, cancellationToken);
            }
            await fileRepository.SaveChangeAsync(cancellationToken);

            return new UpdateStoragePermissionResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
