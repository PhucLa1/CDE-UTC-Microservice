

using Mapster;

namespace Auth.Application.JobTitles.CreateJobTitle
{
    public class CreateJobTitleHandler
        (IBaseRepository<JobTitle> jobTitleRepository)
        : ICommandHandler<CreateJobTitleRequest, CreateJobTitleResponse>
    {
        public async Task<CreateJobTitleResponse> Handle(CreateJobTitleRequest request, CancellationToken cancellationToken)
        {
            var jobTitle = request.Adapt<JobTitle>();
            await jobTitleRepository.AddAsync(jobTitle, cancellationToken);
            await jobTitleRepository.SaveChangeAsync(cancellationToken);
            return new CreateJobTitleResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY }; 
     
        }
    }
}
