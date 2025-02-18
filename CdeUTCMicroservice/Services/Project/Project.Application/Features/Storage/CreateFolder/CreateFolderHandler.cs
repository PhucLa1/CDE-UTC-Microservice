
namespace Project.Application.Features.Storage.CreateFolder
{
    public class CreateFolderHandler
        (IBaseRepository<Folder> folderRepository)
        : ICommandHandler<CreateFolderRequest, CreateFolderResponse>
    {
        public async Task<CreateFolderResponse> Handle(CreateFolderRequest request, CancellationToken cancellationToken)
        {
            var folder = new Folder()
            {
                Name = request.Name,
                ProjectId = request.ProjectId,
                ParentId = request.ParentId
            };

            await folderRepository.AddAsync(folder, cancellationToken);
            await folderRepository.SaveChangeAsync(cancellationToken);

            return new CreateFolderResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
