using BuildingBlocks.Pagination;
using Mapster;

namespace Auth.Application.Languages.GetLanguages
{
    /// <summary>
    /// Dưới đây là xử lí phân trang, bao gồm dùng smartdatatable để có thể tìm kiếm, sắp sếp và phân trang cho dữ liệu đầu ra
    /// </summary>
    /// <param name="languageRepository"></param>
    #region Pagination
    public class GetPaginationLanguagesHandler
        (IBaseRepository<Language> languageRepository)
        : IQueryHandler<GetPaginationLanguagesRequest, ApiResponse<PaginationResult<GetLanguagesResponse>>>
    {
        public async Task<ApiResponse<PaginationResult<GetLanguagesResponse>>> Handle(GetPaginationLanguagesRequest request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PageIndex;
            int pageSize = request.PageSize;

            var pageLanguage = await languageRepository
                .QueryAsync(request, cancellationToken);

            var paginationResult = pageLanguage.Adapt<PaginationResult<GetLanguagesResponse>>();

            return new ApiResponse<PaginationResult<GetLanguagesResponse>> { Data = paginationResult, Message = Message.GET_SUCCESSFULLY };

        }
    }
    #endregion

    /// <summary>
    /// Đây là hàm được sinh ra để list toàn bộ mọi thực thể, mà không cần phân trang
    /// </summary>
    /// <param name="languageRepository"></param>
    #region  No pagination
    public class GetLanguagesHandler
       (IBaseRepository<Language> languageRepository)
       : IQueryHandler<GetLanguagesRequest, ApiResponse<List<GetLanguagesResponse>>>
    {
        public async Task<ApiResponse<List<GetLanguagesResponse>>> Handle(GetLanguagesRequest request, CancellationToken cancellationToken)
        {
            var language = await languageRepository
                .GetAllQueryAble()
                .ToListAsync(cancellationToken);
            var getLanguageResponse = language.Adapt<List<GetLanguagesResponse>>();
            return new ApiResponse<List<GetLanguagesResponse>> { Data = getLanguageResponse, Message = Message.GET_SUCCESSFULLY };

        }
    }
    #endregion
}
