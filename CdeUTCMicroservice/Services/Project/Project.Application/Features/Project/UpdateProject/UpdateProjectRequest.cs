using Microsoft.AspNetCore.Http;

namespace Project.Application.Features.Project.UpdateProject
{
    public class UpdateProjectRequest : ICommand<UpdateProjectResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile Image { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
