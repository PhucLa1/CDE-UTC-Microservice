


namespace Project.Application.Features.Storage.DeleteFolder
{
    public class DeleteFolderHandler
        (IBaseRepository<Folder> folderRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<DeleteFolderRequest, DeleteFolderResponse>
    {
        public async Task<DeleteFolderResponse> Handle(DeleteFolderRequest request, CancellationToken cancellationToken)
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

            folderRepository.Remove(folder);
            await folderRepository.SaveChangeAsync(cancellationToken);

            return new DeleteFolderResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
