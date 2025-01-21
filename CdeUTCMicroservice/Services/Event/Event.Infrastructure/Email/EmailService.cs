


namespace Event.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _serverMailSetting;
        private string templateContent;
        private const string FOLDER = "Email";
        private const string TEMPLATE_FILE = "Template.html";

        public string TemplateContent => templateContent;

        public EmailService(IOptions<EmailSetting> serverMailSetting)
        {
            _serverMailSetting = serverMailSetting.Value;
            templateContent = HandleFile.READ_FILE(FOLDER, TEMPLATE_FILE)
                .Replace("{title}", "CDE UTC")
                .Replace("{currentYear}", DateTime.Now.Year.ToString());
        }
        public async Task SendEmailToRecipient(RecipentEmail emailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_serverMailSetting.EmailUsername));
                email.To.Add(MailboxAddress.Parse(emailRequest.To));
                email.Subject = emailRequest.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = emailRequest.Body };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_serverMailSetting.EmailHost, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_serverMailSetting.EmailUsername, _serverMailSetting.EmailPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task SendEmailToMultipleRecipients(MultiRecipientEmail emailRequests)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_serverMailSetting.EmailUsername));
                foreach (var toRecipient in emailRequests.To)
                {
                    email.To.Add(MailboxAddress.Parse(toRecipient));
                }
                email.Subject = emailRequests.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = emailRequests.Body };

                using var smtp = new SmtpClient();
                smtp.Connect(_serverMailSetting.EmailHost, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_serverMailSetting.EmailUsername, _serverMailSetting.EmailPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
