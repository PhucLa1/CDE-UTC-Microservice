using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Team.GetAllTarget
{
    public class GetAllTargetHandler
        (IBaseRepository<UserProject> userProjectRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetAllTargetRequest, ApiResponse<List<GetAllTargetResponse>>>
    {
        public async Task<ApiResponse<List<GetAllTargetResponse>>> Handle(GetAllTargetRequest request, CancellationToken cancellationToken)
        {
            var userProject = await userProjectRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == request.ProjectId && e.UserProjectStatus == UserProjectStatus.Active)
                .Select(e => new GetAllTargetResponse()
                {
                    Id = e.UserId,
                })
                .ToListAsync(cancellationToken);
            var users = await userGrpc.GetUsersByIds(new GetUserRequestGrpc() { Ids = userProject.Select(e => e.Id).ToList() });
            var targets = (from up in userProject
                                    join u in users on up.Id equals u.Id
                                    select new GetAllTargetResponse()
                                    {
                                        Id = up.Id,
                                        Name = u.FullName,
                                        Email = u.Email,
                                        Url = u.ImageUrl,
                                        IsGroup = false
                                    }).ToList();

            return new ApiResponse<List<GetAllTargetResponse>>() { Data = targets, Message = Message.GET_SUCCESSFULLY };

        }
    }
}
