namespace Project.Application.Features.Types.GetTypes
{
    public class GetTypesRequest : IQuery<ApiResponse<List<GetTypeResponse>>>
    {
        public int ProjectId { get; set; }
    }
}
