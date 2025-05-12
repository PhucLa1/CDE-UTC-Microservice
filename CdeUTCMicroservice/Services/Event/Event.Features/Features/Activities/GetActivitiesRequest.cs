using BuildingBlocks.Enums;

namespace Event.Features.Features.Activities
{
    public class GetActivitiesRequest : IQuery<ApiResponse<List<GetActivitiesResponse>>>
    {
        public int ProjectId { get; set; }
        public List<TypeActivity>? TypeActivities { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int>? CreatedBys { get; set; }
    }
}
