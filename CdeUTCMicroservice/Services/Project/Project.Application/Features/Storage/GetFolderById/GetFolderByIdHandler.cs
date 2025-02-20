using Project.Application.Dtos.Result;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Storage.GetFolderById
{
    public class GetFolderByIdHandler
        (IBaseRepository<FolderComment> folderCommentRepository,
        IBaseRepository<Folder> folderRepository,
        IBaseRepository<FolderHistory> folderHistoryRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetFolderByIdRequest, ApiResponse<GetFolderByIdResponse>>
    {
        public async Task<ApiResponse<GetFolderByIdResponse>> Handle(GetFolderByIdRequest request, CancellationToken cancellationToken)
        {
            //lấy định dạng ngày tháng
            var listIDGrpc = new List<int>() { };
            var currentDateDisplay = folderRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = folderRepository.GetCurrentTimeDisplay();

            var folder = await folderRepository.GetAllQueryAble()
                .Include(e => e.FolderTags)
                .ThenInclude(e => e.Tag)
                .Select(e => new GetFolderByIdResponse()
                {
                    Id = e.Id,
                    Name = e.Name,
                    CreatedAt = e.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    CreatedBy = e.CreatedBy,
                    TagResults = e.FolderTags.Select(e => new TagResult()
                    {
                        Id = e.TagId.Value,
                        Name = e.Tag.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (folder is null)
                throw new NotFoundException(Message.NOT_FOUND);

            var createdByList = new List<int> { folder.CreatedBy }; //List thứ nhất
            var folderComments = await folderCommentRepository.GetAllQueryAble()
                .Where(e => e.FolderId == request.Id)
                .Select(e => new UserCommentResult()
                {
                    Id = e.Id,
                    Content = e.Content,
                    UpdatedBy = e.UpdatedBy,
                    UpdatedAt = e.UpdatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                })
                .ToListAsync(cancellationToken);

            var updatedByList = folderComments.Select(fc => fc.UpdatedBy).Distinct().ToList(); // Distinct IDs //List thứ 2
            //Xử lí phần folder history
            var folderHistories = await folderHistoryRepository.GetAllQueryAble()
                .Where(e => e.FolderId == request.Id)
                .Select(e => new FolderHistoryResult()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Version = e.Version,
                    CreatedAt = e.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    CreatedBy = e.CreatedBy
                })
                .ToListAsync(cancellationToken);

            var createdByFolderHistoryList = folderHistories.Select(fc => fc.CreatedBy).Distinct().ToList(); // Distinct IDs //List thứ 3

            //Ghép các id vào với nhau
            var mergeList = createdByList.Concat(updatedByList).Concat(createdByFolderHistoryList).Distinct().ToList();

            var usersMergeList = await userGrpc
               .GetUsersByIds(new GetUserRequestGrpc { Ids = mergeList });

            if (usersMergeList.Any())
            {
                folder.NameCreatedBy = usersMergeList.First(e => e.Id == folder.CreatedBy).FullName;
                folder.UserCommentResults = folderComments.Select(fc =>
                {
                    var uc = usersMergeList.First(u => u.Id == fc.UpdatedBy); // Find matching user
                    return new UserCommentResult
                    {
                        Id = fc.Id, //Id của comment
                        Content = fc.Content,
                        UpdatedBy = fc.UpdatedBy,
                        UpdatedAt = fc.UpdatedAt,
                        Email = uc.Email, // Handle potential null
                        Name = uc.FullName,
                        AvatarUrl = uc.ImageUrl
                    };
                }).ToList();
                folder.FolderHistoryResults = folderHistories.Select(fc =>
                {
                    var ufh = usersMergeList.First(u => u.Id == fc.CreatedBy); // Find matching user
                    return new FolderHistoryResult
                    {
                        Id = fc.Id, //Id của comment
                        NameCreatedBy = ufh.FullName,
                        Version = fc.Version,
                        CreatedAt = fc.CreatedAt,
                        CreatedBy = fc.CreatedBy,
                        Name = fc.Name
                    };
                }).ToList();
            }
            else
            {
                folder.FolderHistoryResults = new List<FolderHistoryResult>(); // Initialize empty list
                folder.UserCommentResults = new List<UserCommentResult>(); // Initialize empty list
            }
            return new ApiResponse<GetFolderByIdResponse>() { Data = folder, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
