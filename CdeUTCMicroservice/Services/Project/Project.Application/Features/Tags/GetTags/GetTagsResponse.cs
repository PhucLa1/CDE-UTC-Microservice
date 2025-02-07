namespace Project.Application.Features.Tags.GetTags
{
    public class GetTagsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsBlock { get; set; }
    }
}
