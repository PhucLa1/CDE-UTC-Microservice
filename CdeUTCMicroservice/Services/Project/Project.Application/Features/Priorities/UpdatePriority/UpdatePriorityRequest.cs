namespace Project.Application.Features.Priorities.UpdatePriority
{
    public class UpdatePriorityRequest : ICommand<UpdatePriorityResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ColorRGB { get; set; } = string.Empty;
        public int ProjectId { get; set; }
    }
}
