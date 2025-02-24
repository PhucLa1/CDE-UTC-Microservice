namespace Project.Application.Features.Storage.GetFolderTree
{
    public class GetFolderTreeRequest : IQuery<ApiResponse<GetFolderTreeResponse>>
    {
        public int Id { get; set; }
    }
}
