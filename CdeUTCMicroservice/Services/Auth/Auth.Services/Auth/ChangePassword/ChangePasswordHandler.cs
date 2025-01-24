
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Auth.Application.Auth.ChangePassword
{
    public class ChangePasswordHandler
        (IBaseRepository<User> userRepository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<ChangePasswordRequest, ChangePasswordResponse>
    {
        public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAllQueryAble()
                        .Where(e => e.Email == request.Email)
                        .FirstOrDefaultAsync(cancellationToken);

            if(user is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (!user.CanChangePassword)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE_PASSWORD);

            //Hash mật khẩu
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.CanChangePassword = false;
            userRepository.Update(user);
            await userRepository.SaveChangeAsync(cancellationToken);

            //Gửi message sang bên event service
            var eventMessage = new ChangePasswordEvent()
            {
                Email = request.Email,
                Name = user.FirstName + " "+ user.LastName,
                Password = request.Password,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new ChangePasswordResponse() { Data = true, Message = Message.CHANGE_PASSWORD_SUCCESSFULLY };
        }
    }
}
