namespace Project.Application.Features.Team.InviteUser
{
    public class InviteUserRequest : ICommand<InviteUserResponse>
    {
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public int ProjectId { get; set; }
    }
}
