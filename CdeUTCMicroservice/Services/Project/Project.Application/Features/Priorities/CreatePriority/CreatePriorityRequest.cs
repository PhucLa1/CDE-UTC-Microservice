namespace Project.Application.Features.Priorities.CreatePriority
{
    public class CreatePriorityRequest : ICommand<CreatePriorityResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string ColorRGB { get; set; } = string.Empty;
        public int ProjectId { get; set; }
    }
}
