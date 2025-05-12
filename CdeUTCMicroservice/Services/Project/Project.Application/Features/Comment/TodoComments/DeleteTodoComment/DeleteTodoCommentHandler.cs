using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Comment.TodoComments.DeleteTodoComment
{
    public class DeleteTodoCommentHandler
        (IBaseRepository<TodoComment> todoCommentRepository,
        IBaseRepository<Todo> todoRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<DeleteTodoCommentRequest, DeleteTodoCommentResponse>
    {
        public async Task<DeleteTodoCommentResponse> Handle(DeleteTodoCommentRequest request, CancellationToken cancellationToken)
        {
            var todoComment = await todoCommentRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.Id, cancellationToken);
            if (todoComment == null)
            {
                throw new NotFoundException(nameof(TodoComment), request.Id);
            }

            todoCommentRepository.Remove(todoComment);
            await todoCommentRepository.SaveChangeAsync(cancellationToken);
            
            var todo = await todoRepository.GetAllQueryAble().FirstAsync(e => e.Id == todoComment.TodoId, cancellationToken);

            var eventMessage = new CreateActivityEvent()
            {
                Action = "DELETE",
                ResourceId = todoComment.Id,
                Content = "Đã xóa bình luận với nội dung " + todoComment.Content + " ở việc cần làm " + todo.Name,
                TypeActivity = TypeActivity.Comment,
                ProjectId = todoRepository.GetProjectId(),
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new DeleteTodoCommentResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
} 