
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Todos.DeleteTodo
{
    public class DeleteTodoHandler
        (IBaseRepository<Todo> todoRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<DeleteTodoRequest, DeleteTodoResponse>
    {
        public async Task<DeleteTodoResponse> Handle(DeleteTodoRequest request, CancellationToken cancellationToken)
        {
            var todo = await todoRepository.GetAllQueryAble()
                .FirstAsync(e => e.Id == request.Id, cancellationToken);

            todoRepository.Remove(todo);
            await todoRepository.SaveChangeAsync(cancellationToken);

            var activityEvent = new CreateActivityEvent
            {
                Action = "DELETE_TODO",
                ResourceId = todo.Id,
                Content = $"Công việc '{todo.Name}' đã bị xóa.",
                TypeActivity = TypeActivity.Todo,
                ProjectId = todo.ProjectId.Value
            };

            await publishEndpoint.Publish(activityEvent, cancellationToken);

            return new DeleteTodoResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY};
        }
    }
}
