namespace Project.Application.Features.Statuses.DeleteStatus
{
    public class DeleteStatusRequest : ICommand<DeleteStatusResponse>
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
    }
}
