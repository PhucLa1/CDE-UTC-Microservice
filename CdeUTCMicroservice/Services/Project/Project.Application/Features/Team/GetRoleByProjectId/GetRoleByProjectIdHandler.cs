
using MassTransit.Initializers;

namespace Project.Application.Features.Team.GetRoleByProjectId
{
    public class GetRoleByProjectIdHandler
        (IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<ProjectEntity> projectEntityRepository)
        : IQueryHandler<GetRoleByProjectIdRequest, ApiResponse<GetRoleByProjectIdResponse>>
    {
        public async Task<ApiResponse<GetRoleByProjectIdResponse>> Handle(GetRoleByProjectIdRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = userProjectRepository.GetCurrentId();
            var role = await (from up in userProjectRepository.GetAllQueryAble()
                              join pe in projectEntityRepository.GetAllQueryAble() on up.ProjectId equals pe.Id
                              where pe.Id == request.ProjectId && up.UserId == currentUserId
                              select new GetRoleByProjectIdResponse()
                              {
                                  Id = up.UserId,
                                  Role = up.Role,
                                  InvitationPermission = pe.InvitationPermission,
                                  TodoVisibility = pe.TodoVisibility,
                              }).FirstAsync(cancellationToken);

            return new ApiResponse<GetRoleByProjectIdResponse> { Data = role, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
