using Project.Application.Dtos.Result;
using Project.Application.Features.Views.GetViewById;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Storage.GetViewById
{
    public class GetViewByIdHandler
        (IBaseRepository<ViewComment> ViewCommentRepository,
        IBaseRepository<View> ViewRepository,
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
                .Select(e => new GetViewByIdResponse()
                {
                    Id = e.Id,
                    Name = e.Name,
                    CreatedAt = e.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    CreatedBy = e.CreatedBy,
                    TagResults = e.ViewTags.Select(e => new TagResult()
                    {
                        Id = e.TagId.Value,
                        Name = e.Tag.Name
                    }).ToList(),
                    Description = e.Description
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
