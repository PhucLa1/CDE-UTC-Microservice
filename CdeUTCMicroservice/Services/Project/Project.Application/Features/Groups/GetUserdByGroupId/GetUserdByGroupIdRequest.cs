namespace Project.Application.Features.Groups.GetUserdByGroupId
{
    public class GetUserdByGroupIdRequest : IQuery<ApiResponse<List<GetUserdByGroupIdResponse>>>
    {
        public int GroupId { get; set; }
    }
}
