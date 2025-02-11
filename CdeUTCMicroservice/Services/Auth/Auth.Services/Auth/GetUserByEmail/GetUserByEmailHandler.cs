
using Grpc.Core;
using Microsoft.AspNetCore.Http;

namespace Auth.Application.Auth.GetUserByEmail
{
    public class GetUserByEmailHandler
        (IBaseRepository<User> userRepository)
        : IQueryHandler<GetUserByEmailRequest, ApiResponse<GetUserByEmailResponse>>
    {
        public async Task<ApiResponse<GetUserByEmailResponse>> Handle(GetUserByEmailRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAllQueryAble()
                .Where(e => e.Email == request.Email)
                .Select(e => new GetUserByEmailResponse()
                {
                    FullName = e.FirstName + " " + e.LastName,
                    Id = e.Id,
                    Email = e.Email,
                    ImageUrl = Setting.AUTH_HOST + "/User/" + e.ImageUrl,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if(user is null)
                throw new RpcException(new Status(StatusCode.NotFound, Message.NOT_FOUND));

            return new ApiResponse<GetUserByEmailResponse>() { Data = user, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
