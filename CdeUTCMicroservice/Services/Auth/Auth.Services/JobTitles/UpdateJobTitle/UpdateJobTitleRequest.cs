namespace Auth.Application.JobTitles.UpdateJobTitle
{
    public class UpdateJobTitleRequest : ICommand<UpdateJobTitleResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
