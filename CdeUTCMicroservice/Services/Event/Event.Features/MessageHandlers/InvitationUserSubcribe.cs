
namespace Event.Features.MessageHandlers
{
    public class InvitationUserSubcribe
        (IEmailService emailService)
        : IConsumer<InvitationUserEvent>
    {
        public async Task Consume(ConsumeContext<InvitationUserEvent> context)
        {
            var message = context.Message;
            var bodyContentEmail = HandleFile.READ_FILE("Email", "InvitationUser.html")
                                        .Replace("{fullName}", message.FullName)
                                        .Replace("{role}", message.Role)
                                        .Replace("{invitationLink}", "");

            var contentEmail = emailService.TemplateContent.Replace("{content}", bodyContentEmail);

            RecipentEmail email = new RecipentEmail()
            {
                To = message.Email,
                Body = contentEmail,
                Subject = "CDE - Thư mời bạn vào dự án"
            };
            await emailService.SendEmailToRecipient(email);
        }
    }
}
