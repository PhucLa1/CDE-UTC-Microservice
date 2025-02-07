
namespace Project.Application.Features.Tags.CreateTag
{
    public class CreateTagHandler
        (IBaseRepository<Tag> tagRepository)
        : ICommandHandler<CreateTagRequest, CreateTagResponse>
    {
        public async Task<CreateTagResponse> Handle(CreateTagRequest request, CancellationToken cancellationToken)
        {
            var tag = new Tag()
            {
                Name = request.Name,
                ProjectId = request.ProjectId,
            };
            await tagRepository.AddAsync(tag, cancellationToken);
            await tagRepository.SaveChangeAsync(cancellationToken);
            return new CreateTagResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
