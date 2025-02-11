using Auth.Application.Auth.GetUserByEmail;
using Auth.Application.Auth.GetUserByIds;
using Grpc.Core;
using Mapster;
using User.Grpc;

namespace Auth.API.ProtosEndpoint
{
    public class UserProto(IMediator mediator)
        : UserProtoService.UserProtoServiceBase
    {
        public override async Task<GetUserResponse> GetUserByIds(GetUserRequest request, ServerCallContext context)
        {
            var userResponse = await mediator.Send(new GetUserByIdsRequest() { Ids = request.Ids.ToList() });
            var getUserResponse = new GetUserResponse();
            getUserResponse.UserModel.AddRange(userResponse.Data.Adapt<List<UserModel>>());
            return getUserResponse;
        }

        public override async Task<UserModel> GetUserByEmail(GetUserByEmailStore request, ServerCallContext context)
        {
            var userResponse = await mediator.Send(new GetUserByEmailRequest() { Email = request.Email });
            var userModel = userResponse.Data.Adapt<UserModel>();
            return userModel;
        }
    }
}
