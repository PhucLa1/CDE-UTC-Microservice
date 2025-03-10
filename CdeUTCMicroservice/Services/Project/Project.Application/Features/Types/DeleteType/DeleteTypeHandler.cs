﻿
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
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId);

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var typeDelete = await typeRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (typeDelete is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if(typeDelete.IsBlock)
                throw new NotFoundException(Message.FORBIDDEN_CHANGE);

            typeRepository.Remove(typeDelete);
            await typeRepository.SaveChangeAsync(cancellationToken);

            return new DeleteTypeResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
