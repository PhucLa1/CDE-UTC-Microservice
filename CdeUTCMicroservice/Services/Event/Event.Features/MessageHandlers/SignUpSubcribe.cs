namespace Event.Features.MessageHandlers
{
    public class SignUpSubcribe
        (IEmailService emailService)
        : IConsumer<SignUpEvent>
    {
        public async Task Consume(ConsumeContext<SignUpEvent> context)
        {
            var message = context.Message;
            var bodyContentEmail = HandleFile.READ_FILE("Email", "SignUpSuccessfully.html")
                                        .Replace("{Name}", message.FirstName + " " + message.LastName);

            var contentEmail = emailService.TemplateContent.Replace("{content}", bodyContentEmail);

            RecipentEmail email = new RecipentEmail()
            {
                To = message.Email,
                Body = contentEmail,
                Subject = "CDE - Thư thông báo đăng kí thành công"
            };
            await emailService.SendEmailToRecipient(email);
        }
    }
}
