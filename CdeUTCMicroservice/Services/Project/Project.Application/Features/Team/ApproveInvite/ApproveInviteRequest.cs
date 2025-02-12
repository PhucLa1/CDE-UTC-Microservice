namespace Project.Application.Features.Team.ApproveInvite
{
    public class ApproveInviteRequest : ICommand<ApproveInviteResponse>
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
