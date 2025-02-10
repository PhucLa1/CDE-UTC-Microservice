
using Mapster;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Team.GetUsersByProjectId
{
    public class GetUsersByProjectIdHandler
        (IBaseRepository<UserProject> userProjectRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetUsersByProjectIdRequest, ApiResponse<List<GetUsersByProjectIdResponse>>>
    {

        public async Task<ApiResponse<List<GetUsersByProjectIdResponse>>> Handle(GetUsersByProjectIdRequest request, CancellationToken cancellationToken)
        {
            var userProjects = await userProjectRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == request.ProjectId)
                .Select(e => new 
                {
                    Id = e.UserId,
                    UserProjectStatus = e.UserProjectStatus,
                    DateJoined = e.DateJoined,
                    Role = e.Role,
                })
                .ToListAsync(cancellationToken);
            var ids = userProjects.Select(e => e.Id).ToList();
            var usersInfo = await userGrpc.GetUsersByIds(new GetUserRequestGrpc() { Ids = ids});
            var joinedList = userProjects.Join(
                    usersInfo,
                    userProject => userProject.Id,  // Key của userProjects
                    userInfo => userInfo.Id,        // Key của usersInfo
                    (userProject, userInfo) => new GetUsersByProjectIdResponse
                    {
                        Id = userProject.Id,
                        UserProjectStatus = userProject.UserProjectStatus,
                        FullName = userInfo.FullName,  
                        Email = userInfo.Email,
                        DateJoined = userProject.DateJoined,
                        ImageUrl = userInfo.ImageUrl,
                        Role = userProject.Role,
                    }).ToList();

            return new ApiResponse<List<GetUsersByProjectIdResponse>> { Data = joinedList, Message = Message.GET_SUCCESSFULLY };

        }
    }
}
