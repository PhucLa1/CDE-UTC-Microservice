using Project.Application.Dtos.Result;
using Project.Application.Features.Views.GetViewById;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Storage.GetViewById
{
    public class GetViewByIdHandler
        (IBaseRepository<ViewComment> ViewCommentRepository,
        IBaseRepository<View> ViewRepository,
        IBaseRepository<File> fileRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetViewByIdRequest, ApiResponse<GetViewByIdResponse>>
    {
        public async Task<ApiResponse<GetViewByIdResponse>> Handle(GetViewByIdRequest request, CancellationToken cancellationToken)
        {
            //lấy định dạng ngày tháng
            var listIDGrpc = new List<int>() { };
            var currentDateDisplay = ViewRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = ViewRepository.GetCurrentTimeDisplay();

            var View = await ViewRepository.GetAllQueryAble()
                 .Include(e => e.ViewTags)
                     .ThenInclude(e => e.Tag)
                 .Join(fileRepository.GetAllQueryAble(),
                     view => view.FileId,  // Giả sử View có trường FileId
                     file => file.Id,
                     (view, file) => new { view, file }) // Chọn cả View và File
                 .Select(e => new GetViewByIdResponse()
                 {
                     Id = e.view.Id,
                     Name = e.view.Name,
                     CreatedAt = e.view.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                     CreatedBy = e.view.CreatedBy,
                     TagResults = e.view.ViewTags.Where(e => e.Tag != null && e.TagId != null).Select(tag => new TagResult()
                     {
                         Id = tag.TagId.Value,
                         Name = tag.Tag.Name
                     }).ToList(),
                     Description = e.view.Description,
                     Url = e.file.Url // Lấy URL từ File
                 })
                 .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);


            if (View is null)
                throw new NotFoundException(Message.NOT_FOUND);

            var createdByList = new List<int> { View.CreatedBy }; //List thứ nhất
            var ViewComments = await ViewCommentRepository.GetAllQueryAble()
                .Where(e => e.ViewId == request.Id)
                .Select(e => new UserCommentResult()
                {
                    Id = e.Id,
                    Content = e.Content,
                    UpdatedBy = e.UpdatedBy,
                    UpdatedAt = e.UpdatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                })
                .ToListAsync(cancellationToken);

            var updatedByList = ViewComments.Select(fc => fc.UpdatedBy).Distinct().ToList(); // Distinct IDs //List thứ 2
            //Xử lí phần View history
            /*
            var ViewHistories = await ViewHistoryRepository.GetAllQueryAble()
                .Where(e => e.ViewId == request.Id)
                .Select(e => new ViewHistoryResult()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Version = e.Version,
                    CreatedAt = e.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    CreatedBy = e.CreatedBy
                })
                .ToListAsync(cancellationToken);

            var createdByViewHistoryList = ViewHistories.Select(fc => fc.CreatedBy).Distinct().ToList(); // Distinct IDs //List thứ 3

            */
            //Ghép các id vào với nhau
            var mergeList = createdByList
                .Concat(updatedByList)
                //.Concat(createdByViewHistoryList)
                .Distinct().ToList();

            var usersMergeList = await userGrpc
               .GetUsersByIds(new GetUserRequestGrpc { Ids = mergeList });

            if (usersMergeList.Any())
            {
                View.NameCreatedBy = usersMergeList.First(e => e.Id == View.CreatedBy).FullName;
                View.UserCommentResults = ViewComments.Select(fc =>
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
                /*
                View.ViewHistoryResults = ViewHistories.Select(fc =>
                {
                    var ufh = usersMergeList.First(u => u.Id == fc.CreatedBy); // Find matching user
                    return new ViewHistoryResult
                    {
                        Id = fc.Id, //Id của comment
                        NameCreatedBy = ufh.FullName,
                        Version = fc.Version,
                        CreatedAt = fc.CreatedAt,
                        CreatedBy = fc.CreatedBy,
                        Name = fc.Name
                    };
                }).ToList();
                */
            }
            else
            {
                //View.ViewHistoryResults = new List<ViewHistoryResult>(); // Initialize empty list
                View.UserCommentResults = new List<UserCommentResult>(); // Initialize empty list
            }
            return new ApiResponse<GetViewByIdResponse>() { Data = View, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
