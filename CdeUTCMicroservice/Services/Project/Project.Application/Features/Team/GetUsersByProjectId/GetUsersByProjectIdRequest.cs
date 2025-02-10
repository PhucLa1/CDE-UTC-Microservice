namespace Project.Application.Features.Team.GetUsersByProjectId
{
    public class GetUsersByProjectIdRequest : IQuery<ApiResponse<List<GetUsersByProjectIdResponse>>>
    {
        public int ProjectId { get; set; }
    }
}
