namespace Project.Application.Features.Statuses.ResetStatus
{
    public class ResetStatusRequest : ICommand<ResetStatusResponse>
    {
        public Guid ProjectId { get; set; }
    }
}
