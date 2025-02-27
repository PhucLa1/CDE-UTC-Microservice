using Project.Application.Features.Storage.GetAllStorages;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Views.GetAllViews
{
    public class GetAllViewsHandler
        (IBaseRepository<View> viewRepository,
        IBaseRepository<File> fileRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetAllViewsRequest, ApiResponse<List<GetAllViewsResponse>>>
    {
        public async Task<ApiResponse<List<GetAllViewsResponse>>> Handle(GetAllViewsRequest request, CancellationToken cancellationToken)
        {
            var currentDateDisplay = viewRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = viewRepository.GetCurrentTimeDisplay();

            var views = await (from v in viewRepository.GetAllQueryAble().Include(e => e.ViewTags).ThenInclude(e => e.Tag)
                               join f in fileRepository.GetAllQueryAble() on v.FileId equals f.Id
                               where f.ProjectId == request.ProjectId
                               select new GetAllViewsResponse()
                               {
                                   Id = f.Id,
                                   Name = v.Name,
                                   CreatedAt = v.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                                   CreatedBy = v.CreatedBy,
                                   TagNames = v.ViewTags.Select(e => e.Tag.Name).ToList()
                               })
                               .ToListAsync(cancellationToken);

            var users = await userGrpc
                .GetUsersByIds(new GetUserRequestGrpc { Ids = views.Select(e => e.CreatedBy).ToList() });

            var result = (from v in views
                            join u in users on v.CreatedBy equals u.Id
                            select new GetAllViewsResponse()
                            {
                                Id = v.Id,
                                Name = v.Name,
                                CreatedAt = v.CreatedAt,
                                CreatedBy = v.CreatedBy,
                                NameCreatedBy = u.FullName,
                                TagNames = ConvertTagsToView(v.TagNames)
                            }).ToList();

            return new ApiResponse<List<GetAllViewsResponse>> { Data = result, Message = Message.GET_SUCCESSFULLY };
        }


        private List<string> ConvertTagsToView(List<string> tagNames)
        {
            var MAX_COUNT = 25;
            List<string> result = new List<string>();
            var countChars = 0;
            for (int i = 0; i < tagNames.Count; i++)
            {
                countChars += tagNames[i].Count();
                if (countChars <= MAX_COUNT)
                {
                    result.Add(tagNames[i]);
                }
                else
                {
                    result.Add("+ " + (tagNames.Count() - i).ToString());
                    break;
                }
            }

            return result;
        }
    }
}
