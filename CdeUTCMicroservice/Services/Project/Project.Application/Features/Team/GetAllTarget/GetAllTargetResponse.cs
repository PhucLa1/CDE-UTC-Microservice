namespace Project.Application.Features.Team.GetAllTarget
{
    public class GetAllTargetResponse {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Url { get; set; }
        public bool IsGroup { get; set; }
    }
}
