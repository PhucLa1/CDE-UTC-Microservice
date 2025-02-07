namespace Project.Application.Features.Statuses.DeleteStatus
{
    public class DeleteStatusRequest : ICommand<DeleteStatusResponse>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}
