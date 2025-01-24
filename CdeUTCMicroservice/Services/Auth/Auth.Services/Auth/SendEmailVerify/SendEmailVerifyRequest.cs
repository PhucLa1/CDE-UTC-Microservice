namespace Auth.Application.Auth.SendEmailVerify
{
    public class SendEmailVerifyRequest : ICommand<SendEmailVerifyResponse>
    {
        public string Email { get; set; } = string.Empty;
    }
}
