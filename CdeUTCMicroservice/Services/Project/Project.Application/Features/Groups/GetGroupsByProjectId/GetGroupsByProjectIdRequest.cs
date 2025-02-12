namespace Project.Application.Features.Groups.GetGroupsByProjectId
{
    public class GetGroupsByProjectIdRequest : ICommand<ApiResponse<List<GetGroupsByProjectIdResponse>>>
    {
        public int ProjectId { get; set; }
    }
}
