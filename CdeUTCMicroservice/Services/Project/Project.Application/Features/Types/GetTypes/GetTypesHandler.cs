
namespace Project.Application.Features.Types.GetTypes
{
    public class GetTypesHandler
        (IBaseRepository<Type> typeRepository)
        : IQueryHandler<GetTypesRequest, ApiResponse<List<GetTypeResponse>>>
    {
        public async Task<ApiResponse<List<GetTypeResponse>>> Handle(GetTypesRequest request, CancellationToken cancellationToken)
        {
            var types = await typeRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == ProjectId.Of(request.ProjectId))
                .Select(e => new GetTypeResponse
                {
                    Name = e.Name,
                    ImageIconUrl = Setting.PROJECT_HOST + "/Types/" + e.IconImageUrl,
                    Id = e.Id.Value,
                    IsBlock = e.IsBlock
                })
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<GetTypeResponse>> { Data = types, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
