namespace Project.Application.Features.Comment.TodoComments.DeleteTodoComment
{
    public class DeleteTodoCommentHandler
        (IBaseRepository<TodoComment> todoCommentRepository)
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

            return new DeleteTodoCommentResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
} 