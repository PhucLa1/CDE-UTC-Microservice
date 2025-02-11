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
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var statusDelete = await statusRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (statusDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (statusDelete.IsBlock)
                throw new NotFoundException(Message.FORBIDDEN_CHANGE);

            statusRepository.Remove(statusDelete);


            //Trước khi save thì phải chuyền về cái cũ đã
            var status = await statusRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.ProjectId == request.ProjectId);

            if (status is null)
                throw new NotFoundException(Message.FORBIDDEN_CHANGE);

            status.IsDefault = true;
            statusRepository.Update(status);
            await statusRepository.SaveChangeAsync(cancellationToken);

            return new DeleteStatusResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
