namespace Project.Application.Features.Priorities.GetPriorities
{
    public class GetPrioritiesResponse
    {
        public int Id { get; set; }
        public string ColorRGB { get; set; } = string.Empty;
        public bool IsBlock { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
