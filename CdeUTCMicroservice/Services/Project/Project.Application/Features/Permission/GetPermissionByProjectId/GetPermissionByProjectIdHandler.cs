
namespace Project.Application.Features.Permission.GetPermissionByProjectId
{
    public class GetPermissionByProjectIdHandler
        (IBaseRepository<ProjectEntity> projectEntityRepository)
        : IQueryHandler<GetPermissionByProjectIdRequest, ApiResponse<GetPermissionByProjectIdResponse>>
    {
        public async Task<ApiResponse<GetPermissionByProjectIdResponse>> Handle(GetPermissionByProjectIdRequest request, CancellationToken cancellationToken)
        {
            var permission = await projectEntityRepository.GetAllQueryAble()
                .Where(e => e.Id == request.ProjectId)
                .Select(e => new GetPermissionByProjectIdResponse
                {
                    TodoVisibility = e.TodoVisibility,
                    InvitationPermission = e.InvitationPermission,
                })
                .FirstOrDefaultAsync();

            if (permission is null)
                throw new NotFoundException(Message.NOT_FOUND);

            return new ApiResponse<GetPermissionByProjectIdResponse> { Data = permission, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
