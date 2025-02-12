namespace Project.Application.Features.Groups.GetGroupsByProjectId
{
    public class GetGroupsByProjectIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UserCount { get; set; }
    }
}
