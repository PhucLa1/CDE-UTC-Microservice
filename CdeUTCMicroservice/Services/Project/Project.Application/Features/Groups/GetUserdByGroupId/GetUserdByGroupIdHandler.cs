using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Groups.GetUserdByGroupId
{
    public class GetUserdByGroupIdHandler
        (IBaseRepository<UserGroup> userGroupRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetUserdByGroupIdRequest, ApiResponse<List<GetUserdByGroupIdResponse>>>
    {
        public async Task<ApiResponse<List<GetUserdByGroupIdResponse>>> Handle(GetUserdByGroupIdRequest request, CancellationToken cancellationToken)
        {
            var users = await (from ug in userGroupRepository.GetAllQueryAble()
                               join up in userProjectRepository.GetAllQueryAble() on ug.UserId equals up.UserId
                               select new
                               {
                                   UserId = ug.UserId,
                                   DateJoined = ug.DateJoined,
                                   UserProjectStatus = up.UserProjectStatus,
                                   Role = up.Role,
                               })
                                 .ToListAsync(cancellationToken);

            var usersInfo = await userGrpc.GetUsersByIds(new GetUserRequestGrpc() { Ids = users.Select(e => e.UserId).ToList() });

            var userResponse = (from u in users
                               join ui in usersInfo on u.UserId equals ui.Id
                               select new GetUserdByGroupIdResponse
                               {
                                   FullName = ui.FullName,
                                   Id = ui.Id,
                                   Email = ui.Email,
                                   ImageUrl = ui.ImageUrl,
                                   DateJoined = u.DateJoined,
                                   UserProjectStatus = u.UserProjectStatus,
                                   Role = u.Role
                               }).ToList();

            return new ApiResponse<List<GetUserdByGroupIdResponse>> { Data = userResponse, Message = Message.GET_SUCCESSFULLY };



        }
    }
}
