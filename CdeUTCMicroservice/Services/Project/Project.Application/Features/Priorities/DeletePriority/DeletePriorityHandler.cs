namespace Project.Application.Features.Priorities.DeletePriority
{
    public class DeletePriorityHandler
        (IBaseRepository<Priority> priorityRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<DeletePriorityRequest, DeletePriorityResponse>
    {
        public async Task<DeletePriorityResponse> Handle(DeletePriorityRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
          * Admin : được xoa những type nào không phải priority mặc định
          * Member : Không được delete
          */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var priorityDelete = await priorityRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (priorityDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (priorityDelete.IsBlock)
                throw new NotFoundException(Message.FORBIDDEN_CHANGE);

            priorityRepository.Remove(priorityDelete);
            await priorityRepository.SaveChangeAsync(cancellationToken);

            return new DeletePriorityResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
