namespace Project.Application.Features.Types.ResetType
{
    public class ResetTypeRequest : ICommand<ResetTypeResponse>
    {
        public int ProjectId { get; set; }
    }
}
