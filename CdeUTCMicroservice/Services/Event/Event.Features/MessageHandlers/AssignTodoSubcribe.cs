
namespace Event.Features.MessageHandlers
{
    public class AssignTodoSubcribe
        (IEmailService emailService)
        : IConsumer<AssignTodoEvent>
    {
        public async Task Consume(ConsumeContext<AssignTodoEvent> context)
        {
            var message = context.Message;
            for (int i = 0; i < message.UserNames.Count; i++)
            {
                var bodyContentEmail = HandleFile.READ_FILE("Email", "AssignTodo.html")
                            .Replace("{Name}", message.UserNames[i])
                            .Replace("{Email}", message.TaskTitle)
                            .Replace("{DueDate}", message.DueDate)
                            .Replace("{Priority}", message.Priority);
                var contentEmail = emailService.TemplateContent.Replace("{content}", bodyContentEmail);

                RecipentEmail email = new RecipentEmail()
                {
                    To = message.Emails[i],
                    Body = contentEmail,
                    Subject = "CDE - Thư thông báo giao việc cần làm"
                };
                await emailService.SendEmailToRecipient(email);
            }

        }
    }
}
