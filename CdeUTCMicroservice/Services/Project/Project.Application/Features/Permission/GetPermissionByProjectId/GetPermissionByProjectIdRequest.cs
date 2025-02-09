namespace Project.Application.Features.Permission.GetPermissionByProjectId
{
    public class GetPermissionByProjectIdRequest : IQuery<ApiResponse<GetPermissionByProjectIdResponse>>
    {
        public int ProjectId { get; set; }
    }
}
