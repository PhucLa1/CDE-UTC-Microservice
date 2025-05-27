namespace Project.Application.Features.Storage.DeleteFile
{
    public class DeleteFileRequest : ICommand<DeleteFileResponse>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}
