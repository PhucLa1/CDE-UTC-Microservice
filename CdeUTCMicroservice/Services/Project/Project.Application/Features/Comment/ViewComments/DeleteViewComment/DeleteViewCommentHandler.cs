namespace Project.Application.Features.Comment.ViewComments.DeleteViewComment
{
    public class DeleteViewCommentHandler
    (IBaseRepository<ViewComment> viewCommentRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : ICommandHandler<DeleteViewCommentRequest, DeleteViewCommentResponse>
    {
        public async Task<DeleteViewCommentResponse> Handle(DeleteViewCommentRequest request, CancellationToken cancellationToken)
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

            viewCommentRepository.Remove(viewComment);
            await viewCommentRepository.SaveChangeAsync(cancellationToken);

            return new DeleteViewCommentResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
