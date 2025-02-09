namespace Project.Application.Features.Permission.ChangePermission
{
    public class ChangePermissionRequest : ICommand<ChangePermissionResponse>
    {
        public int ProjectId { get; set; }
        public TodoVisibility TodoVisibility { get; set; }
        public InvitationPermission InvitationPermission { get; set; }
    }
}
