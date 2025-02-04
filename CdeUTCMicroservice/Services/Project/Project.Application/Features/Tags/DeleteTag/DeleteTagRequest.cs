namespace Project.Application.Features.Tags.DeleteTag
{
    public class DeleteTagRequest : ICommand<DeleteTagResponse>
    {
        public List<Guid> Ids { get; set; } = new List<Guid> { };
        public Guid ProjectId { get; set; }
    }
}
