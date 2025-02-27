
namespace Project.Application.Features.Comment.ViewComments.CreateViewComment
{
    public class CreateViewCommentHandler
        (IBaseRepository<ViewComment> viewCommentRepository)
        : ICommandHandler<CreateViewCommentRequest, CreateViewCommentResponse>
    {
        public async Task<CreateViewCommentResponse> Handle(CreateViewCommentRequest request, CancellationToken cancellationToken)
        {
            var viewComment = new ViewComment()
            {
                ViewId = request.ViewId,
                Content = request.Content,
            };

            await viewCommentRepository.AddAsync(viewComment, cancellationToken);
            await viewCommentRepository.SaveChangeAsync(cancellationToken);

            return new CreateViewCommentResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
