
namespace Project.Application.Features.Types.DeleteType
{
    public class DeleteTypeHandler
        (IBaseRepository<Type> typeRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<DeleteTypeRequest, DeleteTypeResponse>
    {
        public async Task<DeleteTypeResponse> Handle(DeleteTypeRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
          * Admin : được xoa những type nào không phải type mặc định
          * Member : Không được delete
          */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == ProjectId.Of(request.ProjectId));

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var type = Type.InitData(ProjectId.Of(request.ProjectId))
                .FirstOrDefault(e => e.Id == TypeId.Of(request.Id));
            //Nếu là 1 trong những type mặc định thì sẽ không được xóa

            if (type is not null)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var typeDelete = await typeRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == TypeId.Of(request.Id));

            if (typeDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            typeRepository.Remove(typeDelete);
            await typeRepository.SaveChangeAsync(cancellationToken);

            return new DeleteTypeResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
