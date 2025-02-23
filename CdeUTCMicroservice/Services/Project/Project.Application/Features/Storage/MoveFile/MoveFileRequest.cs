namespace Project.Application.Features.Storage.MoveFile
{
    public class MoveFileRequest : ICommand<MoveFileResponse>
    {
        public int Id { get; set; }
        public int FolderId { get; set; }
    }
}
