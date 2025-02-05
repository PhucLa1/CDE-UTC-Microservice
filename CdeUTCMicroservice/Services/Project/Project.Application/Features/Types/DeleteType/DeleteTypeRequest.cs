namespace Project.Application.Features.Types.DeleteType
{
    public class DeleteTypeRequest : ICommand<DeleteTypeResponse>
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
    }
}
