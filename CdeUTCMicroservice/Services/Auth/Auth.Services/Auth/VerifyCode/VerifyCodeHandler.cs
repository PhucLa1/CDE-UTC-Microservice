
namespace Auth.Application.Auth.VerifyCode
{
    public class VerifyCodeHandler
        (IBaseRepository<User> userRepository)
        : ICommandHandler<VerifyCodeRequest, VerifyCodeResponse>
    {
        public async Task<VerifyCodeResponse> Handle(VerifyCodeRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAllQueryAble()
                        .Where(e => e.Email == request.Email)
                        .FirstOrDefaultAsync(cancellationToken);

            if(user is null)
                throw new NotFoundException(Message.NOT_FOUND);

            //Check xem code và còn hạn sử dụng không
            if(user.Code != request.Code)
                throw new BadRequestException(Message.CODE_NOT_RIGHT);

            else if(DateTime.UtcNow > user.TimeExpired)
                throw new BadRequestException(Message.CODE_EXPIRED);

            //Cho thời hạn và code về rỗng
            user.Code = string.Empty;
            user.TimeExpired = DateTime.MinValue;
            user.CanChangePassword = true;
            userRepository.Update(user);
            await userRepository.SaveChangeAsync(cancellationToken);
            return new VerifyCodeResponse() { Data = true, Message = Message.CODE_VERIFY_SUCCESSFULLY };
        }
    }
}
