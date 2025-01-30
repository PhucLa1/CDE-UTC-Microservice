
namespace Project.Application.Features.Unit.GetUnitByProjectId
{
    public class GetUnitByProjectIdRequest : IQuery<ApiResponse<GetUnitByProjectIdResponse>>
    {
        public Guid ProjectId { get; set; }
    }
}
