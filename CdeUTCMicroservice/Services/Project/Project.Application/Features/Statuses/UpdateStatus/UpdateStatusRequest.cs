using Microsoft.AspNetCore.Http;

namespace Project.Application.Features.Statuses.UpdateStatus
{
    public class UpdateStatusRequest : ICommand<UpdateStatusResponse>
    {
        public int ProjectId { get; set; }
        public string ColorRGB { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsDefault { get; set; }
        public int Id { get; set; }
    }
}
