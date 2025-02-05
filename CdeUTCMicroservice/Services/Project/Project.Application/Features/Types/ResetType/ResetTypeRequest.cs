namespace Project.Application.Features.Types.ResetType
{
    public class ResetTypeRequest : ICommand<ResetTypeResponse>
    {
        public Guid ProjectId { get; set; }
    }
}
