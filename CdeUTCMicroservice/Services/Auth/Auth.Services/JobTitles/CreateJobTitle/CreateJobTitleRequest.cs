

namespace Auth.Application.JobTitles.CreateJobTitle
{
    public class CreateJobTitleRequest : ICommand<CreateJobTitleResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}
