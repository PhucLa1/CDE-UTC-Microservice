
namespace Project.Application.Features.Views.DeleteView
{
    public class DeleteViewHandler(IBaseRepository<View> viewRepository)
        : ICommandHandler<DeleteViewRequest, DeleteViewResponse>
    {
        public async Task<DeleteViewResponse> Handle(DeleteViewRequest request, CancellationToken cancellationToken)
        {
            var view = await viewRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.Id);
            viewRepository.Remove(view);
            await viewRepository.SaveChangeAsync(cancellationToken);

            return new DeleteViewResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
