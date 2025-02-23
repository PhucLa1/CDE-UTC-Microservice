namespace Project.Application.Features.Storage.GetAllFolderDestination
{
    public class GetAllFolderDestinationRequest : IQuery<ApiResponse<List<GetAllFolderDestinationResponse>>>
    {
        public List<int> FileIds { get; set; } = new List<int> { };
        public List<int> FolderIds { get; set; } = new List<int> { };
        public int ParentId {  get; set; }
    }
}
