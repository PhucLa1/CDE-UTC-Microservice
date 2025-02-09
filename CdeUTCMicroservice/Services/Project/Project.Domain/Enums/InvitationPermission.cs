using System.ComponentModel;

namespace Project.Domain.Enums
{
    public enum InvitationPermission
    {
        [Description("User can invite people from other places")]
        UserCanInvite,
        [Description("Only admin can invite")]
        OnlyAdminCanInvite
    }
}
