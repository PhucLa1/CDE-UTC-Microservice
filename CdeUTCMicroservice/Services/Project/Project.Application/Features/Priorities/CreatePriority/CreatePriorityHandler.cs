using Project.Application.Features.Project.CreateProject;
using Project.Application.Features.Types.CreateType;

namespace Project.Application.Features.Priorities.CreatePriority
{
    public class CreatePriorityHandler
         (IBaseRepository<Priority> prorityRepository,
        IBaseRepository<UserProject> userProjectRepository)
         : ICommandHandler<CreatePriorityRequest, CreatePriorityResponse>
    {
        public async Task<CreatePriorityResponse> Handle(CreatePriorityRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được tạo mới priority
            * Member : Không được tạo mới
            */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var priority = new Priority()
            {
                ProjectId = request.ProjectId,
                ColorRGB = request.ColorRGB,
                Name = request.Name,
            };
            await prorityRepository.AddAsync(priority, cancellationToken);
            await prorityRepository.SaveChangeAsync(cancellationToken);
            return new CreatePriorityResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
