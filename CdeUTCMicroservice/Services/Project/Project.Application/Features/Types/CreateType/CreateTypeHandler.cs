namespace Project.Application.Features.Types.CreateType
{
    public class CreateTypeHandler
        (IBaseRepository<Type> typeRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<CreateTypeRequest, CreateTypeResponse>
    {
        public async Task<CreateTypeResponse> Handle(CreateTypeRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
           * Admin : được tạo mới type
           * Member : Không được tạo mới
           */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var type = new Type()
            {
                ProjectId = request.ProjectId,
                IconImageUrl = HandleFile.UPLOAD("Types", request.IconImage),
                Name = request.Name,
            };
            await typeRepository.AddAsync(type, cancellationToken);
            await typeRepository.SaveChangeAsync(cancellationToken);
            return new CreateTypeResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
