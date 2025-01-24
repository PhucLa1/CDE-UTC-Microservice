namespace Event.Features.MessageHandlers
{
    public class ChangePasswordSubcribe
        (IEmailService emailService)
        : IConsumer<ChangePasswordEvent>
    {
        public async Task Consume(ConsumeContext<ChangePasswordEvent> context)
        {
            var message = context.Message;
            var bodyContentEmail = HandleFile.READ_FILE("Email", "ChangePassword.html")
                                        .Replace("{Name}", message.Name)
                                        .Replace("{Email}", message.Email)
                                        .Replace("{Password}", message.Password);

            var contentEmail = emailService.TemplateContent.Replace("{content}", bodyContentEmail);

            RecipentEmail email = new RecipentEmail()
            {
                To = message.Email,
                Body = contentEmail,
                Subject = "CDE - Thư thông báo đã đổi mật khẩu thành công"
            };
            await emailService.SendEmailToRecipient(email);
        }
    }
}
