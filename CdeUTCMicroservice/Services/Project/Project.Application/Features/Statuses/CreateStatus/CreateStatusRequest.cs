namespace Project.Application.Features.Statuses.CreateStatus
{
    public class CreateStatusRequest : ICommand<CreateStatusResponse>
    {
        public int ProjectId { get; set; }
        public string ColorRGB { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsDefault { get; set; } 
    }
}
