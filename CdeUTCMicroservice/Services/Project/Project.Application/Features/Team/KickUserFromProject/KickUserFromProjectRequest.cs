namespace Project.Application.Features.Team.KickUserFromProject
{
    public class KickUserFromProjectRequest : ICommand<KickUserFromProjectResponse>
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
