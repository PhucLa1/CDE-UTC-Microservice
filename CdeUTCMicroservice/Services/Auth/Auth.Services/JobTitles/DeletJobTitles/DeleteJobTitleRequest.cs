namespace Auth.Application.JobTitles.DeletJobTitles
{
    public class DeleteJobTitleRequest : ICommand<DeleteJobTitleResponse>
    {
        public List<Guid> JobTitleIds { get; set; } = new();
    }
}
