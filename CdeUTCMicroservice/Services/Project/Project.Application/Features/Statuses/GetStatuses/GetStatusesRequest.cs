namespace Project.Application.Features.Statuses.GetStatuses
{
    public class GetStatusesRequest : IQuery<ApiResponse<List<GetStatusesResponse>>>
    {
        public Guid ProjectId { get; set; }
    }
}
