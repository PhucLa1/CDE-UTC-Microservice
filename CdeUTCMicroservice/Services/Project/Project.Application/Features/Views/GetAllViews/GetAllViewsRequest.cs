namespace Project.Application.Features.Views.GetAllViews
{
    public class GetAllViewsRequest : IQuery<ApiResponse<List<GetAllViewsResponse>>>
    {
        public int ProjectId { get; set; }
    }
}
