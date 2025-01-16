using BuildingBlocks.Pagination;
using Mapster;

namespace Auth.Application.JobTitles.GetJobTitle
{
    /// <summary>
    /// Dưới đây là xử lí phân trang, bao gồm dùng smartdatatable để có thể tìm kiếm, sắp sếp và phân trang cho dữ liệu đầu ra
    /// </summary>
    /// <param name="jobTitleRepository"></param>
    #region Pagination
    public class GetPaginationJobTitleHandler
        (IBaseRepository<JobTitle> jobTitleRepository)
        : IQueryHandler<GetPaginationJobTitleRequest, ApiResponse<PaginationResult<GetJobTitleResponse>>>
    {
        public async Task<ApiResponse<PaginationResult<GetJobTitleResponse>>> Handle(GetPaginationJobTitleRequest request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PageIndex;
            int pageSize = request.PageSize;

            var pageJobTitle = await jobTitleRepository
                .QueryAsync(request, cancellationToken);

            var paginationResult = pageJobTitle.Adapt<PaginationResult<GetJobTitleResponse>>();

            return new ApiResponse<PaginationResult<GetJobTitleResponse>> { Data = paginationResult, Message = Message.GET_SUCCESSFULLY };

        }
    }
    #endregion

    /// <summary>
    /// Đây là hàm được sinh ra để list toàn bộ mọi thực thể, mà không cần phân trang
    /// </summary>
    /// <param name="jobTitleRepository"></param>
    #region No Pagination
    public class GetJobTitleHandler
        (IBaseRepository<JobTitle> jobTitleRepository)
        : IQueryHandler<GetJobTitleRequest, ApiResponse<List<GetJobTitleResponse>>>
    {
        public async Task<ApiResponse<List<GetJobTitleResponse>>> Handle(GetJobTitleRequest request, CancellationToken cancellationToken)
        {

            var jobTitle = await jobTitleRepository
                .GetAllQueryAble()
                .ToListAsync();

            var getJobTitleResponse = jobTitle.Adapt<List<GetJobTitleResponse>>();

            return new ApiResponse<List<GetJobTitleResponse>> { Data = getJobTitleResponse, Message = Message.GET_SUCCESSFULLY };

        }
    }
    #endregion

}
