using Project.Application.Dtos.Result;
using Project.Application.Extensions;
using Project.Application.Features.Storage.GetFolderById;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Storage.GetFileById
{
    public class GetFileByIdHandler
        (IBaseRepository<FileComment> fileCommentRepository,
        IBaseRepository<File> fileRepository,
        IBaseRepository<FileHistory> fileHistoryRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetFileByIdRequest, ApiResponse<GetFileByIdResponse>>
    {
        

        public async Task<ApiResponse<GetFileByIdResponse>> Handle(GetFileByIdRequest request, CancellationToken cancellationToken)
        {
            var IMAGE_EXTENSION = new List<string>() { ".png", ".jpg", ".jpeg" };
            //lấy định dạng ngày tháng
            var listIDGrpc = new List<int>() { };
            var currentDateDisplay = fileRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = fileRepository.GetCurrentTimeDisplay();

            var file = await fileRepository.GetAllQueryAble()
                .Include(e => e.FileTags)
                .ThenInclude(e => e.Tag)
                .Select(e => new GetFileByIdResponse()
                {
                    Id = e.Id,
                    Name = e.Name,
                    CreatedAt = e.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    CreatedBy = e.CreatedBy,
                    TagResults = e.FileTags.Select(e => new TagResult()
                    {
                        Id = e.TagId.Value,
                        Name = e.Tag.Name
                    }).ToList(),
                    Url = e.Url,
                    Thumbnail = IMAGE_EXTENSION.Contains(e.Extension)
                    ? e.Url
                    : Setting.PROJECT_HOST + "/Extension/" + e.Extension.ConvertToUrl(),
                })
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (file is null)
                throw new NotFoundException(Message.NOT_FOUND);

            var createdByList = new List<int> { file.CreatedBy }; //List thứ nhất

            var fileComments = await fileCommentRepository.GetAllQueryAble()
                .Where(e => e.FileId == request.Id)
                .Select(e => new UserCommentResult()
                {
                    Id = e.Id,
                    Content = e.Content,
                    UpdatedBy = e.UpdatedBy,
                    UpdatedAt = e.UpdatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                })
                .ToListAsync(cancellationToken);

            var updatedByList = fileComments.Select(fc => fc.UpdatedBy).Distinct().ToList(); // Distinct IDs //List thứ 2

            //Xử lí phần file history
            var fileHistories = await fileHistoryRepository.GetAllQueryAble()
                .Where(e => e.FileId == request.Id)
                .Select(e => new FileHistoryResult()
                {
                    Id = e.Id,
                    Name = e.Name + e.Extension,
                    Version = e.FileVersion,
                    CreatedAt = e.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    CreatedBy = e.CreatedBy,
                    ImageUrl = IMAGE_EXTENSION.Contains(e.Extension)
                    ? e.Url
                    : Setting.PROJECT_HOST + "/Extension/" + e.Extension.ConvertToUrl(),
                })
                .ToListAsync(cancellationToken);

            var createdByFileHistoryList = fileHistories.Select(fc => fc.CreatedBy).Distinct().ToList(); // Distinct IDs //List thứ 3

            //Ghép các id vào với nhau
            var mergeList = createdByList.Concat(updatedByList).Concat(createdByFileHistoryList).Distinct().ToList();

            var usersMergeList = await userGrpc
                .GetUsersByIds(new GetUserRequestGrpc { Ids = mergeList });

            if (usersMergeList.Any())
            {
                file.NameCreatedBy = usersMergeList.First(e => e.Id == file.CreatedBy).FullName;
                file.UserCommentResults = fileComments.Select(fc =>
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
                file.FileHistoryResults = fileHistories.Select(fc =>
                {
                    var ufh = usersMergeList.First(u => u.Id == fc.CreatedBy); // Find matching user
                    return new FileHistoryResult
                    {
                        Id = fc.Id, //Id của comment
                        NameCreatedBy = ufh.FullName,
                        Version = fc.Version,
                        CreatedAt = fc.CreatedAt,
                        CreatedBy = fc.CreatedBy,
                        Name = fc.Name,
                        ImageUrl = fc.ImageUrl
                    };
                }).ToList();
            }
            else
            {
                file.FileHistoryResults = new List<FileHistoryResult>(); // Initialize empty list
                file.UserCommentResults = new List<UserCommentResult>(); // Initialize empty list
            }
            return new ApiResponse<GetFileByIdResponse>() { Data = file, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
