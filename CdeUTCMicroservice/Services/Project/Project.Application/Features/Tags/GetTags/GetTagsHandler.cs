

namespace Project.Application.Features.Tags.GetTags
{
    public class GetTagsHandler
        (IBaseRepository<Tag> tagRepository)
        : IQueryHandler<GetTagsRequest, ApiResponse<List<GetTagsResponse>>>
    {
        public async Task<ApiResponse<List<GetTagsResponse>>> Handle(GetTagsRequest request, CancellationToken cancellationToken)
        {
            var tags = await tagRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == ProjectId.Of(request.ProjectId))
                .Select(e => new GetTagsResponse
                {
                    Id = e.Id.Value,
                    Name = e.Name,
                })
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<GetTagsResponse>> { Data = tags, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
