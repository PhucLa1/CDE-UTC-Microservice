namespace Project.Application.Features.Views.DeleteView
{
    public class DeleteViewRequest : ICommand<DeleteViewResponse>
    {
        public int Id { get; set; }
    }
}
