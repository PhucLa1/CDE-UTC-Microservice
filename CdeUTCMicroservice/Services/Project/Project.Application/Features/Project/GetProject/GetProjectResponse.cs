

namespace Project.Application.Features.Project.GetProject
{
    public class GetProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = default!;
    }
}
