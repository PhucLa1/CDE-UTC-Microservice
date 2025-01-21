using Event.Shared.DTOs.Models;

namespace Event.Infrastructure.Email
{
    public interface IEmailService
    {
        string TemplateContent { get; }
        Task SendEmailToRecipient(RecipentEmail emailRequest);
        Task SendEmailToMultipleRecipients(MultiRecipientEmail emailRequests);
    }
}
