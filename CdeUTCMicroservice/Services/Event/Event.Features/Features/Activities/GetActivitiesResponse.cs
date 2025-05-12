using BuildingBlocks.Enums;

namespace Event.Features.Features.Activities
{
    public class GetActivitiesResponse
    {
        public int Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public int ResourceId { get; set; } //Tác nhân bị tác động
        public string Content { get; set; } = default!;
        public TypeActivity TypeActivity { get; set; }
        public int ProjectId { get; set; }
        public string CreatedAt { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
