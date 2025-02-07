namespace Project.Application.Features.Tags.CreateTag
{
    public class CreateTagRequest : ICommand<CreateTagResponse>
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
