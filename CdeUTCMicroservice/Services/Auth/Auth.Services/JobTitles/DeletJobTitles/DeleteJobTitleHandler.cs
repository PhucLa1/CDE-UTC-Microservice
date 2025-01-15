
namespace Auth.Application.JobTitles.DeletJobTitles
{
    public class DeleteJobTitleHandler
        (IBaseRepository<JobTitle> jobTitleRepository)
        : ICommandHandler<DeleteJobTitleRequest, DeleteJobTitleResponse>
    {
        public async Task<DeleteJobTitleResponse> Handle(DeleteJobTitleRequest request, CancellationToken cancellationToken)
        {
            var jobTitle = await jobTitleRepository
                .GetAllQueryAble()
                .Where(e => request.JobTitleIds.Contains(e.Id))
                .ToListAsync(cancellationToken);

            if (jobTitle.Count != request.JobTitleIds.Count)
                throw new NotFoundException(Message.NOT_FOUND);

            //Xóa 
            jobTitleRepository.RemoveRange(jobTitle);
            await jobTitleRepository.SaveChangeAsync(cancellationToken);
            return new DeleteJobTitleResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
