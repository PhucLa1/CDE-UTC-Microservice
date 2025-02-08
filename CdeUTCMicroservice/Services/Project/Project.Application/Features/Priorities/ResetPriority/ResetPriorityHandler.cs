namespace Project.Application.Features.Priorities.ResetPriority
{
    public class ResetPriorityHandler
    (IBaseRepository<Priority> priorityRepository,
    IBaseRepository<UserProject> userProjectRepository)
    : ICommandHandler<ResetPriorityRequest, ResetPriorityResponse>
    {
        public async Task<ResetPriorityResponse> Handle(ResetPriorityRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được xoa những priority nào không phải priority mặc định
            * Member : Không được delete
            */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);


            var priorityDelete = priorityRepository.GetAllQueryAble()
                .Where(e => e.IsBlock == false);

            if (priorityDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            //Xóa hết đi
            priorityRepository.RemoveRange(priorityDelete);

            await priorityRepository.SaveChangeAsync(cancellationToken);
            return new ResetPriorityResponse() { Data = true, Message = Message.RESET_SUCCESSFULLY };
        }
    }
}
