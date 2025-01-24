

namespace Event.Features.MessageHandlers
{
    public class SendEmailVerifySubcribe
        (IEmailService emailService)
        : IConsumer<SendEmailVerifyEvent>
    {
        public async Task Consume(ConsumeContext<SendEmailVerifyEvent> context)
        {
            var message = context.Message;
            var bodyContentEmail = HandleFile.READ_FILE("Email", "SendEmailVerify.html")
                                        .Replace("{Code}", message.Code)
                                        .Replace("{ExpiredTime}", message.ExpiredTime.ToString());

            var contentEmail = emailService.TemplateContent.Replace("{content}", bodyContentEmail);

            RecipentEmail email = new RecipentEmail()
            {
                To = message.Email,
                Body = contentEmail,
                Subject = "CDE - Thư thông báo đã nhận được mã code"
            };
            await emailService.SendEmailToRecipient(email);
        }
    }
}
