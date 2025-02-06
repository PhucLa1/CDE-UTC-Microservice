namespace Project.Application.Features.Statuses.DeleteStatus
{
    public class DeleteStatusHandler
         (IBaseRepository<Status> statusRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<DeleteStatusRequest, DeleteStatusResponse>
    {
        public async Task<DeleteStatusResponse> Handle(DeleteStatusRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được xoa những status nào không phải status mặc định
            * Member : Không được delete
             */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == ProjectId.Of(request.ProjectId));

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var statusDelete = await statusRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == StatusId.Of(request.Id));

            if (statusDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (statusDelete.IsBlock)
                throw new NotFoundException(Message.FORBIDDEN_CHANGE);

            statusRepository.Remove(statusDelete);
            await statusRepository.SaveChangeAsync(cancellationToken);

            return new DeleteStatusResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
