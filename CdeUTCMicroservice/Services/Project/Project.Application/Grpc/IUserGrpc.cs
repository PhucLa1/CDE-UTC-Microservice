

using Project.Application.Grpc.GrpcRequest;
using Project.Application.Grpc.GrpcResponse;

namespace Project.Application.Grpc
{
    public interface IUserGrpc
    {
        Task<List<GetUserResponseGrpc>> GetUsersByIds(GetUserRequestGrpc getUserRequestGrpc);
    }
}
