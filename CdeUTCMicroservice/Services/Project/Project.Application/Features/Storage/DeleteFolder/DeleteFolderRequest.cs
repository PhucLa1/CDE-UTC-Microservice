namespace Project.Application.Features.Storage.DeleteFolder
{
    public class DeleteFolderRequest : ICommand<DeleteFolderResponse>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}
