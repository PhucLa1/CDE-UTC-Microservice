namespace Project.Application.Features.Priorities.UpdatePriority
{
    public class UpdatePriorityHandler
         (IBaseRepository<Priority> priorityRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<UpdatePriorityRequest, UpdatePriorityResponse>
    {
        public async Task<UpdatePriorityResponse> Handle(UpdatePriorityRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được sửa những priority nào không phải priority mặc định
            * Member : Không được tạo mới
            */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var priorityUpdate = await priorityRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (priorityUpdate is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (priorityUpdate.IsBlock)
                throw new NotFoundException(Message.FORBIDDEN_CHANGE);

            priorityUpdate.Name = request.Name;
            priorityUpdate.ColorRGB = request.ColorRGB;
            priorityRepository.Update(priorityUpdate);
            await priorityRepository.SaveChangeAsync(cancellationToken);

            return new UpdatePriorityResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
