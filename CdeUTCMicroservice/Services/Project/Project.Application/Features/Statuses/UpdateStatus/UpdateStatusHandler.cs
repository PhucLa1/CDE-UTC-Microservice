
namespace Project.Application.Features.Statuses.UpdateStatus
{
    public class UpdateStatusHandler
        (IBaseRepository<Status> statusRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<UpdateStatusRequest, UpdateStatusResponse>
    {
        public async Task<UpdateStatusResponse> Handle(UpdateStatusRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được fix status
            * Member : Không được fix
            */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == ProjectId.Of(request.ProjectId));

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var statusUpdate = await statusRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == StatusId.Of(request.Id));

            if (statusUpdate is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (statusUpdate.IsBlock)
                throw new NotFoundException(Message.FORBIDDEN_CHANGE);

            //Nếu đầu vào là isDefault là true thì mấy cái đằng trước sẽ là false hết
            var statues = await statusRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == ProjectId.Of(request.ProjectId))
                .ToListAsync(cancellationToken);
            statues.ForEach(e => e.IsDefault = false);
            statusRepository.UpdateMany(statues);


            statusUpdate.IsDefault = request.IsDefault;
            statusUpdate.ColorRGB = request.ColorRGB;
            statusUpdate.Name = request.Name;

            statusRepository.Update(statusUpdate);
            await statusRepository.SaveChangeAsync(cancellationToken);

            return new UpdateStatusResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };


        }
    }
}
