namespace Project.Application.Features.Statuses.ResetStatus
{
    public class ResetStatusRequest : ICommand<ResetStatusResponse>
    {
        public int ProjectId { get; set; }
    }
}
