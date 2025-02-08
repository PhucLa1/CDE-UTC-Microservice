namespace Project.Application.Features.Priorities.ResetPriority
{
    public class ResetPriorityRequest : ICommand<ResetPriorityResponse>
    {
        public int ProjectId { get; set; }
    }
}
