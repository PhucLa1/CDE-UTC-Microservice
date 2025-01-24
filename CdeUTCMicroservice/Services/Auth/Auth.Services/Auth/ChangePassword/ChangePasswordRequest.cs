namespace Auth.Application.Auth.ChangePassword
{
    public class ChangePasswordRequest : ICommand<ChangePasswordResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RePassword { get; set; } = string.Empty;
    }
}
