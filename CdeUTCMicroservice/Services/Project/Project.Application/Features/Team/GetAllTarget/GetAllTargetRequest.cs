namespace Project.Application.Features.Team.GetAllTarget
{
    public class GetAllTargetRequest : IQuery<ApiResponse<List<GetAllTargetResponse>>>
    {
        public int ProjectId { get; set; }
    }
}
