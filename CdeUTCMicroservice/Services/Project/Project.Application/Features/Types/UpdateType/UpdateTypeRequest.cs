using Microsoft.AspNetCore.Http;

namespace Project.Application.Features.Types.UpdateType
{
    public class UpdateTypeRequest : ICommand<UpdateTypeResponse>
    {
        public Guid ProjectId { get; set; } = default!;
        public IFormFile IconImage { get; set; } = default!;
        public string Name { get; set; } = default!;
        public Guid Id { get; set; }
    }
}
