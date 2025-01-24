
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Auth.Application.Auth.SendEmailVerify
{
    public class SendEmailVerifyHandler
        (IBaseRepository<User> userRepository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<SendEmailVerifyRequest, SendEmailVerifyResponse>
    {
        public async Task<SendEmailVerifyResponse> Handle(SendEmailVerifyRequest request, CancellationToken cancellationToken)
        {
            //Gen code ảo ra
            Random random = new Random();
            string code = "";
            for (int i = 0; i < 6; i++)
            {
                code += random.Next(0, 10).ToString();
            }
            var user = await userRepository.GetAllQueryAble()
                            .Where(e => e.Email == request.Email)
                            .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                throw new NotFoundException(Message.NOT_FOUND);

            //Thay đổi giá trị Code, TimeExpired
            user.Code = code;
            user.TimeExpired = DateTime.UtcNow.AddMinutes(15);
            userRepository.Update(user);
            await userRepository.SaveChangeAsync(cancellationToken);

            //Gửi message sang EVENT SERVICE

            var eventMessage = new SendEmailVerifyEvent()
            {
                Code = code,
                ExpiredTime = user.TimeExpired,
                Email = request.Email,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);
            return new SendEmailVerifyResponse() { Data = true, Message = Message.SEND_CODE_SUCCESSFULLY };
        }
    }
}
