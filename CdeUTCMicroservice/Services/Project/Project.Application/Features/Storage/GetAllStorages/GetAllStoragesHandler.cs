
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;
using System.Linq;

namespace Project.Application.Features.Storage.GetAllStorages
{
    public class GetAllStoragesHandler
        (IBaseRepository<Folder> folderRepository,
        IBaseRepository<File> fileRepository,
        IUserGrpc userGrpc,
        IBaseRepository<Tag> tagRepository,
        IBaseRepository<FolderTag> folderTagRepository)
        : IQueryHandler<GetAllStoragesRequest, ApiResponse<List<GetAllStoragesResponse>>>
    {
        public async Task<ApiResponse<List<GetAllStoragesResponse>>> Handle(GetAllStoragesRequest request, CancellationToken cancellationToken)
        {
            //lấy định dạng ngày tháng
            var currentDateDisplay = folderRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = folderRepository.GetCurrentTimeDisplay();

            var folders = await folderRepository.GetAllQueryAble()
                .Include(e => e.FolderTags)
                .ThenInclude(e => e.Tag)
                .Where(e => e.ProjectId == request.ProjectId && e.ParentId == request.ParentId)
                .Select(e => new GetAllStoragesResponse()
                {
                    Id = e.Id,
                    IsFile = false,
                    Name = e.Name,
                    CreatedAt = e.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    CreatedBy = e.CreatedBy,
                    TagNames = e.FolderTags.Select(e => e.Tag.Name).ToList()
                })
                .ToListAsync(cancellationToken);

            var users = await userGrpc
                .GetUsersByIds(new GetUserRequestGrpc { Ids = folders.Select(e => e.CreatedBy).ToList() });

            folders = (from f in folders
                       join u in users on f.CreatedBy equals u.Id
                       select new GetAllStoragesResponse()
                       {
                           Id = f.Id,
                           IsFile = false,
                           Name = f.Name,
                           CreatedAt = f.CreatedAt,
                           CreatedBy = f.CreatedBy,
                           NameCreatedBy = u.FullName,
                       }).ToList();

            return new ApiResponse<List<GetAllStoragesResponse>> { Data = folders, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
