
namespace Project.Application.Features.Unit.GetUnitByProjectId
{
    public class GetUnitByProjectIdRequest : IQuery<ApiResponse<GetUnitByProjectIdResponse>>
    {
        public int ProjectId { get; set; }
    }
}
