
namespace Project.Application.Features.Statuses.CreateStatus
{
    public class CreateStatusHandler
        (IBaseRepository<Status> statusRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<CreateStatusRequest, CreateStatusResponse>
    {
        public async Task<CreateStatusResponse> Handle(CreateStatusRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được tạo mới status
            * Member : Không được tạo mới
            */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            //Nếu đầu vào là isDefault là true thì mấy cái đằng trước sẽ là false hết
            var statues = await statusRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == request.ProjectId)
                .ToListAsync(cancellationToken);

            statues.ForEach(e => e.IsDefault = false);
            statusRepository.UpdateMany(statues);

            var status = new Status()
            {
                Name = request.Name,
                IsDefault = request.IsDefault,
                ColorRGB = request.ColorRGB,
                ProjectId = request.ProjectId,
            };
            await statusRepository.AddAsync(status, cancellationToken);
            await statusRepository.SaveChangeAsync(cancellationToken);
            return new CreateStatusResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
