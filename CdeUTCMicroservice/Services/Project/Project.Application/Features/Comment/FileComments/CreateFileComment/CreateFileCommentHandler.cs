namespace Project.Application.Features.Comment.FileComments.CreateFileComment
{
    public class CreateFileCommentHandler
        (IBaseRepository<FileComment> fileCommentRepository)
        : ICommandHandler<CreateFileCommentRequest, CreateFileCommentResponse>
    {
        public async Task<CreateFileCommentResponse> Handle(CreateFileCommentRequest request, CancellationToken cancellationToken)
        {
            var fileComment = new FileComment()
            {
                FileId = request.FileId,
                Content = request.Content,
            };

            await fileCommentRepository.AddAsync(fileComment, cancellationToken);
            await fileCommentRepository.SaveChangeAsync(cancellationToken);
            return new CreateFileCommentResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
