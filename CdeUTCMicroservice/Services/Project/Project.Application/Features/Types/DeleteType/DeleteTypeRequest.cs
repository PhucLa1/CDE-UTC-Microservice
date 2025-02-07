namespace Project.Application.Features.Types.DeleteType
{
    public class DeleteTypeRequest : ICommand<DeleteTypeResponse>
    {
        public int ProjectId { get; set; }
        public int Id { get; set; }
    }
}
