using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Comment.TodoComments.CreateTodoComment
{
    public class CreateTodoCommentHandler
        (IBaseRepository<TodoComment> todoCommentRepository,
        IBaseRepository<Todo> todoRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<CreateTodoCommentRequest, CreateTodoCommentResponse>
    {
        public async Task<CreateTodoCommentResponse> Handle(CreateTodoCommentRequest request, CancellationToken cancellationToken)
        {
            var projectId = todoCommentRepository.GetProjectId();
            var todoComment = new TodoComment()
            {
                TodoId = request.TodoId,
                Content = request.Content,
            };

            await todoCommentRepository.AddAsync(todoComment, cancellationToken);
            await todoCommentRepository.SaveChangeAsync(cancellationToken);

            var todo = await todoRepository.GetAllQueryAble().FirstAsync(e => e.Id == todoComment.TodoId);
            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = todoComment.Id,
                Content = "Đã tạo bình luận với nội dung " + request.Content + " ở việc cần làm " + todo.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = projectId,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);
            return new CreateTodoCommentResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
} 