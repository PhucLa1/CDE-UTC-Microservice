namespace Project.Application.Features.Comment.ViewComments.UpdateViewComment
{
    public class UpdateViewCommentHandler
    (IBaseRepository<ViewComment> viewCommentRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<UpdateViewCommentRequest, UpdateViewCommentResponse>
    {
        public async Task<UpdateViewCommentResponse> Handle(UpdateViewCommentRequest request, CancellationToken cancellationToken)
        {
            var userCurrentId = userProjectRepository.GetCurrentId();
            var userProject = await userProjectRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.UserId == userCurrentId && e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active);

            var viewComment = await viewCommentRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || viewComment is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (userProject.Role is not Role.Admin && viewComment.CreatedBy != userCurrentId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            viewComment.Content = request.Content;
            viewCommentRepository.Update(viewComment);
            await viewCommentRepository.SaveChangeAsync(cancellationToken);

            return new UpdateViewCommentResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
