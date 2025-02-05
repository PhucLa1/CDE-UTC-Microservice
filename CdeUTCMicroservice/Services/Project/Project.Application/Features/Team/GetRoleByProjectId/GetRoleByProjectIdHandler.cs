
using MassTransit.Initializers;

namespace Project.Application.Features.Team.GetRoleByProjectId
{
    public class GetRoleByProjectIdHandler
        (IBaseRepository<UserProject> userProjectRepository)
        : IQueryHandler<GetRoleByProjectIdRequest, GetRoleByProjectIdResponse>
    {
        public async Task<GetRoleByProjectIdResponse> Handle(GetRoleByProjectIdRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = userProjectRepository.GetCurrentId();
            var role = await userProjectRepository.GetAllQueryAble()
                .FirstAsync(x => x.UserId == currentUserId && x.ProjectId == ProjectId.Of(request.ProjectId))
                .Select(e => e.Role);

            return new GetRoleByProjectIdResponse { Data = role, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
