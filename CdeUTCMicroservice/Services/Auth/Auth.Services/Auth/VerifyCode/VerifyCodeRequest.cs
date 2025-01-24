

namespace Auth.Application.Auth.VerifyCode
{
    public class VerifyCodeRequest : ICommand<VerifyCodeResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
