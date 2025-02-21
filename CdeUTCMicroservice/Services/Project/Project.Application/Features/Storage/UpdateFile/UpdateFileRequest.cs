namespace Project.Application.Features.Storage.UpdateFile
{
    public class UpdateFileRequest : ICommand<UpdateFileResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? ProjectId { get; set; }
        public List<int> TagIds { get; set; } = new List<int> { };
    }
}
