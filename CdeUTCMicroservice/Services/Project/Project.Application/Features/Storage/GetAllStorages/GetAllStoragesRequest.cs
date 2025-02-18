namespace Project.Application.Features.Storage.GetAllStorages
{
    public class GetAllStoragesRequest : IQuery<ApiResponse<List<GetAllStoragesResponse>>>
    {
        public int ParentId { get; set; }
        public int ProjectId { get; set; }
    }
}
