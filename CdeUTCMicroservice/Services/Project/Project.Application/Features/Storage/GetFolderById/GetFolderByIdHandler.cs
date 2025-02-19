using Project.Application.Dtos.Result;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Storage.GetFolderById
{
    public class GetFolderByIdHandler
        (IBaseRepository<FolderComment> folderCommentRepository,
        IBaseRepository<Folder> folderRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetFolderByIdRequest, ApiResponse<GetFolderByIdResponse>>
    {
        public async Task<ApiResponse<GetFolderByIdResponse>> Handle(GetFolderByIdRequest request, CancellationToken cancellationToken)
        {
            //lấy định dạng ngày tháng
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

            var createdByList = new List<int> { folder.CreatedBy };

            var usersFolder = await userGrpc
               .GetUsersByIds(new GetUserRequestGrpc { Ids = createdByList });
            folder.NameCreatedBy = usersFolder.First().FullName;

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

            var updatedByList = folderComments.Select(fc => fc.UpdatedBy).Distinct().ToList(); // Distinct IDs

            if (updatedByList.Any()) // Only call gRPC if there are users to fetch
            {
                var userComments = await userGrpc.GetUsersByIds(new GetUserRequestGrpc { Ids = updatedByList });

                folder.UserCommentResults = folderComments.Select(fc =>
                {
                    var uc = userComments.First(u => u.Id == fc.UpdatedBy); // Find matching user
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
            }
            else
            {
                folder.UserCommentResults = new List<UserCommentResult>(); // Initialize empty list
            }

            return new ApiResponse<GetFolderByIdResponse>() { Data = folder, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
