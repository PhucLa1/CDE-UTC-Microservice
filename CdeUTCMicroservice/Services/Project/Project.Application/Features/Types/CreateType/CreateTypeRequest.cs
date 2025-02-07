using Microsoft.AspNetCore.Http;

namespace Project.Application.Features.Types.CreateType
{
    public class CreateTypeRequest : ICommand<CreateTypeResponse>
    {
        public int ProjectId { get; set; } = default!;
        public IFormFile IconImage { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
