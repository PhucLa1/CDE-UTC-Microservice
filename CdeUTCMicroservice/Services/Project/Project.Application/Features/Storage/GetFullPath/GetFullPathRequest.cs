namespace Project.Application.Features.Storage.GetFullPath
{
    public class GetFullPathRequest : IQuery<ApiResponse<List<GetFullPathResponse>>>
    {
        public int FolderId { get; set; }
    }
}
