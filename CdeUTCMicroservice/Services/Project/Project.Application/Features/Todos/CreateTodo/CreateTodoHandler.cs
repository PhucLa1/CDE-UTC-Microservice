
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Initializers;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;
using System.Collections.Generic;

namespace Project.Application.Features.Todos.CreateTodo
{
    public class CreateTodoHandler
        (IBaseRepository<Todo> todoRepository,
        IBaseRepository<ViewTodo> viewTodoRepository,
        IBaseRepository<FileTodo> fileTodoRepository,
        IPublishEndpoint publishEndpoint,
        IBaseRepository<Group> groupRepository,
        IBaseRepository<UserGroup> userGroupRepository,
        IUserGrpc userGrpc,
        IBaseRepository<TodoTag> todoTagRepository,
        IBaseRepository<Priority> priorityRepository)
        : ICommandHandler<CreateTodoRequest, CreateTodoResponse>
    {
        public async Task<CreateTodoResponse> Handle(CreateTodoRequest request, CancellationToken cancellationToken)
        {
            var transaction = await todoRepository.BeginTransactionAsync(cancellationToken);
            var todo = new Todo()
            {
                ProjectId = request.ProjectId,
                Name = request.Name,
                Description = request.Description,
                DueDate = request.DueDate,
                StartDate = request.StartDate,
                PriorityId = request.PriorityId,
                StatusId = request.StatusId,
                TypeId = request.TypeId,
            };

            if(request.AssignTo != null)
            {
                var userIds = new List<int>();
                todo.AssignTo = request.AssignTo;
                todo.IsAssignToGroup = request.IsAssignToGroup;

                if(request.IsAssignToGroup == 1)
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

            await todoRepository.AddAsync(todo, cancellationToken);
            await todoRepository.SaveChangeAsync(cancellationToken);

            //Thêm phần tags
            if (request.TagIds != null)
            {
                var newListTags = new List<TodoTag>();
                foreach (var tagId in request.TagIds)
                {
                    var todoTag = new TodoTag()
                    {
                        TodoId = todo.Id,
                        TagId = tagId
                    };
                    newListTags.Add(todoTag);
                }

                await todoTagRepository.AddRangeAsync(newListTags, cancellationToken);
                await todoRepository.SaveChangeAsync(cancellationToken);
            }

            //Thêm mới files
            if (request.FileIds != null)
            {
                var newListFiles = new List<FileTodo>();
                foreach (var fileId in request.FileIds)
                {
                    var fileTodo = new FileTodo()
                    {
                        TodoId = todo.Id,
                        FileId = fileId
                    };
                    newListFiles.Add(fileTodo);
                }

                await fileTodoRepository.AddRangeAsync(newListFiles, cancellationToken);
                await fileTodoRepository.SaveChangeAsync(cancellationToken);
            }


            //Thêm mới views
            if (request.ViewIds != null)
            {
                var newListTodos = new List<ViewTodo>();
                foreach (var viewId in request.ViewIds)
                {
                    var viewTodo = new ViewTodo()
                    {
                        TodoId = todo.Id,
                        ViewId = viewId
                    };
                    newListTodos.Add(viewTodo);
                }

                await viewTodoRepository.AddRangeAsync(newListTodos, cancellationToken);
                await viewTodoRepository.SaveChangeAsync(cancellationToken);
            }


            await todoRepository.CommitTransactionAsync(transaction, cancellationToken);

            var activityEvent = new CreateActivityEvent
            {
                Action = "CREATE_TODO",
                ResourceId = todo.Id,
                Content = $"Công việc '{todo.Name}' đã được tạo.",
                TypeActivity = TypeActivity.Todo,
                ProjectId = todo.ProjectId.Value
            };

            await publishEndpoint.Publish(activityEvent, cancellationToken);
            return new CreateTodoResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
