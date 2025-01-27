

using Microsoft.AspNetCore.Http;

namespace Project.Application.Features.Project.CreateProject
{
    public class CreateProjectRequest : ICommand<CreateProjectResponse>
    {
        public string Name { get; set; } = default!;
        public IFormFile Image { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = default!;
    }
}
