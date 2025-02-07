namespace Auth.Application.JobTitles.DeletJobTitles
{
    public class DeleteJobTitleRequest : ICommand<DeleteJobTitleResponse>
    {
        public List<int> JobTitleIds { get; set; } = new();
    }
}
