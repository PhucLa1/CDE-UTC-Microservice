namespace Project.Application.Features.Tags.GetTags
{
    public class GetTagsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsBlock { get; set; }
    }
}
