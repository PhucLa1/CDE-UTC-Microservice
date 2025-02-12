
namespace Project.Application.Features.Team.ApproveInvite
{
    public class ApproveInviteHandler
        (IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<ApproveInviteRequest, ApproveInviteResponse>
    {
        public async Task<ApproveInviteResponse> Handle(ApproveInviteRequest request, CancellationToken cancellationToken)
        {
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == request.UserId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            userProject.UserProjectStatus = UserProjectStatus.Active;
            userProjectRepository.Update(userProject);
            await userProjectRepository.SaveChangeAsync(cancellationToken);
            return new ApproveInviteResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
