namespace Project.Application.Features.Statuses.GetStatuses
{
    public class GetStatusesResponse
    {
        public string Name { get; set; } = string.Empty;
        public bool IsBlock { get; set; }
        public string ColorRGB { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public Guid Id { get; set; }
    }
}
