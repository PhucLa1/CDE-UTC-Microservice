namespace Project.Application.Features.Storage.CreateFolder
{
    public class CreateFolderRequest : ICommand<CreateFolderResponse>
    {
        public string Name { get; set; } = default!;
        public int? ProjectId { get; set; }
        public int ParentId { get; set; } = default!;
    }
}
