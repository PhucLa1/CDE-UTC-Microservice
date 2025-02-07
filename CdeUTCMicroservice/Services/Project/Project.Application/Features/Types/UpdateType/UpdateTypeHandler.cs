
namespace Project.Application.Features.Types.UpdateType
{
    public class UpdateTypeHandler
         (IBaseRepository<Type> typeRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<UpdateTypeRequest, UpdateTypeResponse>
    {
        public async Task<UpdateTypeResponse> Handle(UpdateTypeRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được sửa những type nào không phải type mặc định
            * Member : Không được tạo mới
            */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var typeUpdate = await typeRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (typeUpdate is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if(typeUpdate.IsBlock)
                throw new NotFoundException(Message.FORBIDDEN_CHANGE);

            typeUpdate.Name = request.Name;
            if (request.IconImage.Length > 0)
                typeUpdate.IconImageUrl = HandleFile.UPLOAD("Types", request.IconImage);
            typeRepository.Update(typeUpdate);
            await typeRepository.SaveChangeAsync(cancellationToken);

            return new UpdateTypeResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
