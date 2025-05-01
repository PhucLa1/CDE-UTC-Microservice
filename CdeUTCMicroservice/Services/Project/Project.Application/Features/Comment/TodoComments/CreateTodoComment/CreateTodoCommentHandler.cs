namespace Project.Application.Features.Comment.TodoComments.CreateTodoComment
{
    public class CreateTodoCommentHandler
        (IBaseRepository<TodoComment> todoCommentRepository)
        : ICommandHandler<CreateTodoCommentRequest, CreateTodoCommentResponse>
    {
        public async Task<CreateTodoCommentResponse> Handle(CreateTodoCommentRequest request, CancellationToken cancellationToken)
        {
            var todoComment = new TodoComment()
            {
                TodoId = request.TodoId,
                Content = request.Content,
            };

            await todoCommentRepository.AddAsync(todoComment, cancellationToken);
            await todoCommentRepository.SaveChangeAsync(cancellationToken);
            return new CreateTodoCommentResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
} 