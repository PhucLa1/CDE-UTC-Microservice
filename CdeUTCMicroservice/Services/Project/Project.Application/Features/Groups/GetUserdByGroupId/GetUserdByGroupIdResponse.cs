namespace Project.Application.Features.Groups.GetUserdByGroupId
{
    public class GetUserdByGroupIdResponse
    {
        public string FullName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string DateJoined { get; set; } = string.Empty;
        public UserProjectStatus UserProjectStatus { get; set; }
        public Role Role { get; set; }
    }
}
