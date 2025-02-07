
using Project.Application.Features.Types.ResetType;

namespace Project.Application.Features.Statuses.ResetStatus
{
    public class ResetStatusHandler
        (IBaseRepository<Status> statusRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<ResetStatusRequest, ResetStatusResponse>
    {
        public async Task<ResetStatusResponse> Handle(ResetStatusRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
           * Admin : được xoa những type nào không phải status mặc định
           * Member : Không được delete
           */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);


            var statusDelete = statusRepository.GetAllQueryAble();

            if (statusDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            //Xóa hết đi
            statusRepository.RemoveRange(statusDelete);

            //Thêm mới
            await statusRepository.AddRangeAsync(Status.InitData(request.ProjectId), cancellationToken);

            await statusRepository.SaveChangeAsync(cancellationToken);
            return new ResetStatusResponse() { Data = true, Message = Message.RESET_SUCCESSFULLY };
        }
    }
}
