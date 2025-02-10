namespace Auth.Application.Auth.GetUserByIds
{
    public class GetUserByIdsRequest : IQuery<ApiResponse<List<GetUserByIdsResponse>>>
    {
        public List<int> Ids { get; set; } = new List<int>();
    }
}
