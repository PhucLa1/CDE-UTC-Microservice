using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Initializers;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Todos.UpdateTodo
{
    public class UpdateTodoHandler
        (IBaseRepository<Todo> todoRepository,
        IBaseRepository<ViewTodo> viewTodoRepository,
        IBaseRepository<FileTodo> fileTodoRepository,
        IPublishEndpoint publishEndpoint,
        IBaseRepository<Group> groupRepository,
        IBaseRepository<UserGroup> userGroupRepository,
        IUserGrpc userGrpc,
        IBaseRepository<TodoTag> todoTagRepository,
        IBaseRepository<Priority> priorityRepository)
        : ICommandHandler<UpdateTodoRequest, UpdateTodoResponse>
    {
        public async Task<UpdateTodoResponse> Handle(UpdateTodoRequest request, CancellationToken cancellationToken)
        {
            var todo = await todoRepository.GetAllQueryAble()
                .FirstAsync(e => e.Id == request.Id, cancellationToken);

            var transaction = await todoRepository.BeginTransactionAsync(cancellationToken);
            //Cập nhật
            todo.TypeId = request.TypeId;
            todo.StatusId = request.StatusId;
            todo.PriorityId = request.PriorityId;
            todo.StartDate = request.StartDate;
            todo.DueDate = request.DueDate;
            todo.Name = request.Name;
            todo.Description = request.Description;
            
            //Xóa phần fileTodo, viewTodo, tagTodo
            var fileTodos = await fileTodoRepository.GetAllQueryAble()
                .Where(e => e.TodoId == request.Id)
                .ToListAsync();
            var viewTodos = await viewTodoRepository.GetAllQueryAble()
                .Where(e => e.TodoId == request.Id)
                .ToListAsync();
            var tagTodos = await todoTagRepository.GetAllQueryAble()
                .Where(e => e.TodoId == request.Id)
                .ToListAsync();
            fileTodoRepository.RemoveRange(fileTodos);
            viewTodoRepository.RemoveRange(viewTodos);
            todoTagRepository.RemoveRange(tagTodos);
            await todoRepository.SaveChangeAsync(cancellationToken);

            //Thêm lại
            var fileTodoAdds = new List<FileTodo>();
            var viewTodoAdds = new List<ViewTodo>();
            var tagTodoAdds = new List<TodoTag>();
            if (request.FileIds != null)
            {
                foreach (var fileId in request.FileIds)
                {
                    fileTodoAdds.Add(new FileTodo()
                    {
                        FileId = fileId,
                        TodoId = request.Id,
                    });
                }
            }

            if (request.ViewIds != null)
            {
                foreach (var viewId in request.ViewIds)
                {
                    viewTodoAdds.Add(new ViewTodo()
                    {
                        ViewId = viewId,
                        TodoId = request.Id,
                    });
                }
            }

            if(request.TagIds != null)
            {
                foreach (var tagId in request.TagIds)
                {
                    tagTodoAdds.Add(new TodoTag()
                    {
                        TagId = tagId,
                        TodoId = request.Id,
                    });
                }
            }
            await fileTodoRepository.AddRangeAsync(fileTodoAdds, cancellationToken);
            await viewTodoRepository.AddRangeAsync(viewTodoAdds, cancellationToken);
            await todoTagRepository.AddRangeAsync(tagTodoAdds, cancellationToken);
            await todoRepository.SaveChangeAsync(cancellationToken);

            //Check xem có đổi assign to không 
            if (request.AssignTo != null && (todo.AssignTo != request.AssignTo || todo.IsAssignToGroup != request.IsAssignToGroup))
            {
                var userIds = new List<int>();
                todo.AssignTo = request.AssignTo;
                todo.IsAssignToGroup = request.IsAssignToGroup;

                if (request.IsAssignToGroup == 1)
                {
                    var userGroupIds = await (from ug in userGroupRepository.GetAllQueryAble()
                                              join g in groupRepository.GetAllQueryAble() on ug.GroupId equals g.Id
                                              where g.ProjectId == request.ProjectId && ug.GroupId == request.AssignTo
                                              select ug.UserId
                                            ).ToListAsync(cancellationToken);

                    userIds.AddRange(userGroupIds);
                    todo.IsAssignToGroup = request.IsAssignToGroup;
                }
                else
                {
                    userIds.Add(request.AssignTo);
                }

                var users = await userGrpc.GetUsersByIds(new GetUserRequestGrpc { Ids = userIds });

                //Gửi mail cho người đó
                //Gửi message sang bên event service
                var eventMessage = new AssignTodoEvent()
                {
                    UserNames = users.Select(e => e.FullName).ToList(),
                    Emails = users.Select(e => e.Email).ToList(),
                    TaskTitle = request.Name,
                    DueDate = request.DueDate.ToShortDateString(),
                    Priority = await priorityRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.PriorityId).Select(e => e.Name)
                };
                await publishEndpoint.Publish(eventMessage, cancellationToken);
            }


            //Lưu lại
            await todoRepository.SaveChangeAsync(cancellationToken);
            await todoRepository.CommitTransactionAsync(transaction, cancellationToken);

            var activityEvent = new CreateActivityEvent
            {
                Action = "UPDATE_TODO",
                ResourceId = todo.Id,
                Content = $"Công việc '{todo.Name}' đã được cập nhật.",
                TypeActivity = TypeActivity.Todo,
                ProjectId = request.ProjectId
            };

            await publishEndpoint.Publish(activityEvent, cancellationToken);

            return new UpdateTodoResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
