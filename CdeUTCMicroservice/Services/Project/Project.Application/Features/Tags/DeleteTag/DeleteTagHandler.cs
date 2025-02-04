namespace Project.Application.Features.Tags.DeleteTag
{
    public class DeleteTagHandler
        (IBaseRepository<Tag> tagRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<DeleteTagRequest, DeleteTagResponse>
    {
        public async Task<DeleteTagResponse> Handle(DeleteTagRequest request, CancellationToken cancellationToken)
        {
            /* Quy đinh: 
             * Admin : được xóa tất cả các tag
             * Member : Không được xóa
             */

            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == ProjectId.Of(request.ProjectId));

            if (userProject is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var ids = request.Ids.Select(tagId => TagId.Of(tagId)).ToList();

            var tags = await tagRepository.GetAllQueryAble()
                .Where(e => ids.Contains(e.Id))
                .ToListAsync(cancellationToken);

            tagRepository.RemoveRange(tags);
            await tagRepository.SaveChangeAsync(cancellationToken);
            return new DeleteTagResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
