using BuildingBlocks.Pagination;

namespace Auth.Application.Languages.GetLanguages
{
    #region Pagination
    public class GetPaginationLanguagesRequest : PaginationRequest, IQuery<ApiResponse<PaginationResult<GetLanguagesResponse>>>
    {

    }
    #endregion


    #region No pagination
    public class GetLanguagesRequest : IQuery<ApiResponse<List<GetLanguagesResponse>>>
    {

    }
    #endregion
}
