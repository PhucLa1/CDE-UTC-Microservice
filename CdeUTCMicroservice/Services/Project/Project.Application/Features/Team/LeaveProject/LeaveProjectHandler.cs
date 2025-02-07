

namespace Project.Application.Features.Team.LeaveProject
{
    public class LeaveProjectHandler
        (IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<LeaveProjectRequest, LeaveProjectResponse>
    {
        public async Task<LeaveProjectResponse> Handle(LeaveProjectRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(x => x.UserId == currentUserId && x.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            userProjectRepository.Remove(userProject);
            await userProjectRepository.SaveChangeAsync(cancellationToken);
             
            return new LeaveProjectResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
