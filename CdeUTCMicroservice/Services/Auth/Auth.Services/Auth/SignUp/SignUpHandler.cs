
using BuildingBlocks.Messaging.Events;
using FluentValidation;
using Mapster;
using MassTransit;

namespace Auth.Application.Auth.SignUp
{
    internal class SignUpHandler
        (IBaseRepository<User> userRepository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<SignUpRequest, SignUpResponse>
    {
        public async Task<SignUpResponse> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<User>();
            var email = await userRepository
                .GetAllQueryAble()
                .Where(e => e.Email == user.Email)
                .Select(e => e.Email)
                .FirstOrDefaultAsync();
            if (email is not null)
                throw new BadRequestException("Email đã tồn tại rồi");
            //Mã hóa mật khẩu
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await userRepository.AddAsync(user, cancellationToken);
            await userRepository.SaveChangeAsync(cancellationToken);

            //Gửi message đến event service
            var signUpEvent = user.Adapt<SignUpEvent>();
            await publishEndpoint.Publish(signUpEvent, cancellationToken);

            return new SignUpResponse() { Data = true, Message = Message.SIGNUP_SUCCESSFULLY };
        }
    }
}
