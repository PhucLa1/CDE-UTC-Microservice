namespace Project.Application.Features.Team.GetRoleByProjectId
{
    public class GetRoleByProjectIdRequest : IQuery<GetRoleByProjectIdResponse>
    {
        public Guid ProjectId { get; set; }
    }
}
