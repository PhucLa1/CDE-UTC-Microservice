


namespace Project.Application.Features.Team.LeaveProject
{
    public class LeaveProjectRequest : ICommand<LeaveProjectResponse>
    {
        public int ProjectId { get; set; }
    }
}
