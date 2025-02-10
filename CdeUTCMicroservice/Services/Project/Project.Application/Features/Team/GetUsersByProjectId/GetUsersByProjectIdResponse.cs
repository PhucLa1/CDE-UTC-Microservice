namespace Project.Application.Features.Team.GetUsersByProjectId
{
    public class GetUsersByProjectIdResponse
    {
        public string FullName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime DateJoined { get; set; }
        public UserProjectStatus UserProjectStatus { get; set; }
        public Role Role { get; set; }
    }
}
