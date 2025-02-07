namespace Project.Application.Features.Tags.DeleteTag
{
    public class DeleteTagRequest : ICommand<DeleteTagResponse>
    {
        public List<int> Ids { get; set; } = new List<int> { };
        public int ProjectId { get; set; }
    }
}
