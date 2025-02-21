namespace Project.Application.Features.Comment.FileComments.UpdateFileComment
{
    public class UpdateFileCommentHandler
     (IBaseRepository<FileComment> fileCommentRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<UpdateFileCommentRequest, UpdateFileCommentResponse>
    {
        public async Task<UpdateFileCommentResponse> Handle(UpdateFileCommentRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            var fileComment = await fileCommentRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || fileComment is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin && fileComment.CreatedBy != userCurrentId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            fileComment.Content = request.Content;
            fileCommentRepository.Update(fileComment);
            await fileCommentRepository.SaveChangeAsync(cancellationToken);

            return new UpdateFileCommentResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
