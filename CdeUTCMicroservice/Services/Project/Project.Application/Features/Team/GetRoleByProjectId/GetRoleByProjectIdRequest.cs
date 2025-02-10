namespace Project.Application.Features.Team.GetRoleByProjectId
{
    public class GetRoleByProjectIdRequest : IQuery<ApiResponse<GetRoleByProjectIdResponse>>
    {
        public int ProjectId { get; set; }
    }
}
