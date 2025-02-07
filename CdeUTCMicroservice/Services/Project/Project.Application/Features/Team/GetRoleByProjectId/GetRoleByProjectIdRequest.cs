namespace Project.Application.Features.Team.GetRoleByProjectId
{
    public class GetRoleByProjectIdRequest : IQuery<GetRoleByProjectIdResponse>
    {
        public int ProjectId { get; set; }
    }
}
