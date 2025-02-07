
namespace Project.Application.Features.Types.ResetType
{
    public class ResetTypeHandler
        (IBaseRepository<Type> typeRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<ResetTypeRequest, ResetTypeResponse>
    {
        public async Task<ResetTypeResponse> Handle(ResetTypeRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được xoa những type nào không phải type mặc định
            * Member : Không được delete
            */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);


            var typeDelete = typeRepository.GetAllQueryAble();

            if (typeDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            //Xóa hết đi
            typeRepository.RemoveRange(typeDelete);

            //Thêm mới
            await typeRepository.AddRangeAsync(Type.InitData(request.ProjectId), cancellationToken);

            await typeRepository.SaveChangeAsync(cancellationToken);
            return new ResetTypeResponse() { Data = true, Message = Message.RESET_SUCCESSFULLY };
        }
    }
}
