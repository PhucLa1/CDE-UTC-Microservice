namespace Project.Application.Features.Tags.GetTags
{
    public class GetTagsRequest : IQuery<ApiResponse<List<GetTagsResponse>>>
    {
        public Guid ProjectId { get; set; }
    }
}
