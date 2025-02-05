
using Mapster;

namespace Auth.Application.Auth.GetInfo
{
    public class GetInfoHandler
        (IBaseRepository<User> userRepository)
        : IQueryHandler<GetInfoRequest, ApiResponse<GetInfoResponse>>
    {
        public async Task<ApiResponse<GetInfoResponse>> Handle(GetInfoRequest request, CancellationToken cancellationToken)
        {
            var currentId = userRepository.GetDbContext.GetCurrentUserId();
            var user = await userRepository.GetAllQueryAble()
                        .FirstOrDefaultAsync(x => x.Id == currentId);

            if (user is null)
                throw new NotFoundException(Message.NOT_FOUND);

            var userInfo = user.Adapt<GetInfoResponse>();
            userInfo.ImageUrl = Setting.AUTH_HOST + "/User/" + userInfo.ImageUrl;
            return new ApiResponse<GetInfoResponse> { Data = userInfo, Message = Message.LOGIN_SUCCESSFULLY };
        }
    }
}
