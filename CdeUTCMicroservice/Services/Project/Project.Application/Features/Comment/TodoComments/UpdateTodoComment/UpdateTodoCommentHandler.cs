using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Transports;
using Project.Domain.Entities;

namespace Project.Application.Features.Comment.TodoComments.UpdateTodoComment
{
    public class UpdateTodoCommentHandler
        (IBaseRepository<TodoComment> todoCommentRepository,
        IBaseRepository<Todo> todoRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateTodoCommentRequest, UpdateTodoCommentResponse>
    {
        public async Task<UpdateTodoCommentResponse> Handle(UpdateTodoCommentRequest request, CancellationToken cancellationToken)
        {
            var todoComment = await todoCommentRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.Id, cancellationToken);
            if (todoComment == null)
            {
                throw new NotFoundException(nameof(TodoComment), request.Id);
            }

            var oldComment = todoComment.Content;
            todoComment.Content = request.Content;
            todoCommentRepository.Update(todoComment);
            await todoCommentRepository.SaveChangeAsync(cancellationToken);

            var todo = await todoRepository.GetAllQueryAble().FirstAsync(e => e.Id == todoComment.TodoId);
            var eventMessage = new CreateActivityEvent()
            {
                Action = "UPDATE",
                ResourceId = todoComment.Id,
                Content = "Đã sửa bình luận từ nội dung " + oldComment + " sang nội dung " + request.Content + " ở việc cần làm " + todo.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = todoRepository.GetProjectId(),
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new UpdateTodoCommentResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
} 