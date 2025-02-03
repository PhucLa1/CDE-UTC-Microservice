


namespace Project.Application.Features.Team.LeaveProject
{
    public class LeaveProjectRequest : ICommand<LeaveProjectResponse>
    {
        public Guid ProjectId { get; set; }
    }
}
