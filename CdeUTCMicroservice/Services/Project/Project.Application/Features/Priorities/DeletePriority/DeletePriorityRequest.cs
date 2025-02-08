namespace Project.Application.Features.Priorities.DeletePriority
{
    public class DeletePriorityRequest : ICommand<DeletePriorityResponse>
    {
        public int ProjectId { get; set; }
        public int Id { get; set; }
    }
}
