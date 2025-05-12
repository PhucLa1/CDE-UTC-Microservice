using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Comment.FileComments.CreateFileComment
{
    public class CreateFileCommentHandler
        (IBaseRepository<FileComment> fileCommentRepository,
        IBaseRepository<File> fileRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<CreateFileCommentRequest, CreateFileCommentResponse>
    {
        public async Task<CreateFileCommentResponse> Handle(CreateFileCommentRequest request, CancellationToken cancellationToken)
        {
            var projectId = fileRepository.GetProjectId();
            var fileComment = new FileComment()
            {
                FileId = request.FileId,
                Content = request.Content,
            };

            await fileCommentRepository.AddAsync(fileComment, cancellationToken);
            await fileCommentRepository.SaveChangeAsync(cancellationToken);
            var file = await fileRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.FileId);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = fileComment.Id,
                Content = "Đã tạo mới binh luận với nội dung " + request.Content + " ở tệp " + file.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = projectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new CreateFileCommentResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
