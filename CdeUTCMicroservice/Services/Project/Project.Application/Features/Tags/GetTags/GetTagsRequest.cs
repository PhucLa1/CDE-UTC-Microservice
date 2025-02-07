namespace Project.Application.Features.Tags.GetTags
{
    public class GetTagsRequest : IQuery<ApiResponse<List<GetTagsResponse>>>
    {
        public int ProjectId { get; set; }
    }
}
