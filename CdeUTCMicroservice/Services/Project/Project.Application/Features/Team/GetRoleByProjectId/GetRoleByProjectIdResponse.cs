namespace Project.Application.Features.Team.GetRoleByProjectId
{
    public class GetRoleByProjectIdResponse
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public InvitationPermission InvitationPermission { get; set; }
        public TodoVisibility TodoVisibility { get; set; }
    }
}
