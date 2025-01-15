
namespace Auth.Application.JobTitles.UpdateJobTitle
{
    public class UpdateJobTitleHandler
        (IBaseRepository<JobTitle> jobTitleRepository)
        : ICommandHandler<UpdateJobTitleRequest, UpdateJobTitleResponse>
    {
        public async Task<UpdateJobTitleResponse> Handle(UpdateJobTitleRequest request, CancellationToken cancellationToken)
        {
            var jobTitle = await jobTitleRepository
                .GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if(jobTitle is null)
                throw new NotFoundException(Message.NOT_FOUND);

            jobTitle.Name = request.Name;
            jobTitleRepository.Update(jobTitle);
            await jobTitleRepository.SaveChangeAsync(cancellationToken);

            return new UpdateJobTitleResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY }; 

        }
    }
}
