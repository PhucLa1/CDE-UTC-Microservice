using Mapster;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;
using Project.Application.Grpc.GrpcResponse;
using System.Collections.Generic;
using User.Grpc;

namespace Project.API.Grpc
{
    public class UserGrpc
        (UserProtoService.UserProtoServiceClient userProto) : IUserGrpc
    {
        
        public async Task<List<GetUserResponseGrpc>> GetUsersByIds(GetUserRequestGrpc getUserRequestGrpc)
        {
            var getUserRequest = new GetUserRequest { Ids = { getUserRequestGrpc.Ids } };  // Chuyển đổi dữ liệu đơn giản
            var getUserResponse = await userProto.GetUserByIdsAsync(getUserRequest);

            // Dùng Adapt trực tiếp để chuyển đổi và trả về kết quả
            return getUserResponse.UserModel.Adapt<List<GetUserResponseGrpc>>();
        }
    }
}
