namespace Project.Application.Features.Tags.UpdateTag
{
    public class UpdateTagHandler
        (IBaseRepository<Tag> tagRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<UpdateTagRequest, UpdateTagResponse>
    {
        public async Task<UpdateTagResponse> Handle(UpdateTagRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
            * Admin : được fix tất cả các tag
            * Member : Không được fix
            */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == ProjectId.Of(request.ProjectId));

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var tag = await tagRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == TagId.Of(request.Id));

            if (tag is null)
                throw new NotFoundException(Message.NOT_FOUND);

            tag.Name = request.Name;
            tagRepository.Update(tag);
            await tagRepository.SaveChangeAsync(cancellationToken);
            return new UpdateTagResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
