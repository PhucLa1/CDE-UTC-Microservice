using BuildingBlocks.Pagination;

namespace Auth.Application.JobTitles.GetJobTitle
{
    #region Pagination
    public class GetPaginationJobTitleRequest : PaginationRequest, IQuery<ApiResponse<PaginationResult<GetJobTitleResponse>>>
    {

    }
    #endregion

    #region No Pagination
    public class GetJobTitleRequest : IQuery<ApiResponse<List<GetJobTitleResponse>>>
    {

    }
    #endregion
}
