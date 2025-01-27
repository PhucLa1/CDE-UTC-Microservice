using BuildingBlocks.ApiResponse;

namespace Project.Application.Features.Project.GetProject
{
    public class GetProjectRequest : IQuery<ApiResponse<List<GetProjectResponse>>>
    {
    }
}
