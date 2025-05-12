

using Event.Infrastructure.Grpc.GrpcRequest;
using Event.Infrastructure.Grpc.GrpcResponse;

namespace Event.Infrastructure.Grpc
{
    public interface IUserGrpc
    {
        Task<List<GetUserResponseGrpc>> GetUsersByIds(GetUserRequestGrpc getUserRequestGrpc);
        Task<GetUserResponseGrpc> GetUserByEmail(GetUserByEmailRequestGrpc getUserRequestGrpc);
    }
}
