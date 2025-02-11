namespace Project.Application.Features.Team.ChangeRole
{
    public class ChangeRoleRequest : ICommand<ChangeRoleResponse>
    {
        public int ProjectId { get; set; }
        public Role Role { get; set; }
        public int UserId { get; set; }
    }
}
