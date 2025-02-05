namespace Project.Application.Features.Types.GetTypes
{
    public class GetTypeResponse
    {
        public string ImageIconUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid Id { get; set; }
    }
}
