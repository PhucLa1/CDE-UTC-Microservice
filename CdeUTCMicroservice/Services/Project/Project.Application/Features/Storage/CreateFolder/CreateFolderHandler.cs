
using MassTransit.Initializers;

namespace Project.Application.Features.Storage.CreateFolder
{
    public class CreateFolderHandler
        (IBaseRepository<Folder> folderRepository)
        : ICommandHandler<CreateFolderRequest, CreateFolderResponse>
    {
        public async Task<CreateFolderResponse> Handle(CreateFolderRequest request, CancellationToken cancellationToken)
        {

            //Check xem ở nhừng folder cùng cấp có trùng tên không
            var folders = await folderRepository.GetAllQueryAble()
                .Where(e => e.ParentId == request.ParentId && e.Name == request.Name)
                .FirstOrDefaultAsync(cancellationToken);

            if (folders is not null) //Tức là đã có folder tên như thế ở cùng cấp
                throw new BadRequestException(Message.IS_EXIST_FOLDER);

            string fullPath = "";
            string fullPathName = "";
            if (request.ParentId is not 0) //Tức là không phải ngoài cùng gốc
            {
                var folderParentFullPath = await folderRepository.GetAllQueryAble()
                    .Where(e => e.Id == request.ParentId)
                    .Select(e => new
                    {
                        FullPath = e.FullPath,
                        FullPathName = e.FullPathName
                    })
                    .FirstOrDefaultAsync(cancellationToken); // Đưa FirstOrDefaultAsync() vào cuối

                if (folderParentFullPath is null)
                    throw new NotFoundException(Message.NOT_FOUND);

                fullPath = folderParentFullPath.FullPath;
                fullPathName = folderParentFullPath.FullPathName;
            }
            var folder = new Folder()
            {
                Name = request.Name,
                ProjectId = request.ProjectId,
                ParentId = request.ParentId,
                FullPathName = fullPathName + "/" + request.Name
            };

            using var transaction = await folderRepository.BeginTransactionAsync(cancellationToken);

            //Bắt đầy 1 transaction
            await folderRepository.AddAsync(folder, cancellationToken);
            await folderRepository.SaveChangeAsync(cancellationToken);

            folder.FullPath = fullPath + "/" + folder.Id;
            folderRepository.Update(folder);
            await folderRepository.SaveChangeAsync(cancellationToken);

            //Kết thúc 1 transaction
            await folderRepository.CommitTransactionAsync(transaction, cancellationToken);

            return new CreateFolderResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
