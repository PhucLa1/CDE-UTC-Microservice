namespace Project.Application.Features.Priorities.GetPriorities
{
    public class GetPrioritiesRequest : IQuery<ApiResponse<List<GetPrioritiesResponse>>>
    {
        public int ProjectId { get; set; }
    }
}
