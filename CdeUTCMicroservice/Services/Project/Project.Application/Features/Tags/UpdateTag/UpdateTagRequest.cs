

namespace Project.Application.Features.Tags.UpdateTag
{
    public class UpdateTagRequest : ICommand<UpdateTagResponse>
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid Id { get; set; }
    }
}
