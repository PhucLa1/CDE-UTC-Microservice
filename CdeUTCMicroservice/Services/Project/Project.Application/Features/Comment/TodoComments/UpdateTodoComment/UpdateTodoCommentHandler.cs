namespace Project.Application.Features.Comment.TodoComments.UpdateTodoComment
{
    public class UpdateTodoCommentHandler
        (IBaseRepository<TodoComment> todoCommentRepository)
        : ICommandHandler<UpdateTodoCommentRequest, UpdateTodoCommentResponse>
    {
        public async Task<UpdateTodoCommentResponse> Handle(UpdateTodoCommentRequest request, CancellationToken cancellationToken)
        {
            var todoComment = await todoCommentRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.Id, cancellationToken);
            if (todoComment == null)
            {
                throw new NotFoundException(nameof(TodoComment), request.Id);
            }

            todoComment.Content = request.Content;
            todoCommentRepository.Update(todoComment);
            await todoCommentRepository.SaveChangeAsync(cancellationToken);

            return new UpdateTodoCommentResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
} 