
using Mapster;

namespace Auth.Application.JobTitles.GetJobTitle
{
    public class GetJobTitleHandler
        (IBaseRepository<JobTitle> jobTitleRepository)
        : IQueryHandler<GetJobTitleRequest, GetJobTitleResponse>
    {
        public async Task<GetJobTitleResponse> Handle(GetJobTitleRequest request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PageIndex ?? 1;
            int pageSize = request.PageSize ?? 10;


            var pageJobTitle = await jobTitleRepository
                .GetPaginatedAsync(pageIndex, pageSize, cancellationToken);

            var pageJobTitleModels = pageJobTitle.Adapt<IEnumerable<JobTitleModel>>();

            return new GetJobTitleResponse { Data = pageJobTitleModels, Message = Message.GET_SUCCESSFULLY };

        }
    }
}
