namespace Auth.Application.JobTitles.GetJobTitle
{
    public class GetJobTitleRequest : IQuery<GetJobTitleResponse>
    {
        public int? PageIndex { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
