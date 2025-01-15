

namespace Auth.Application.Auth.Login
{
    public class LoginRequest() : ICommand<LoginResponse>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
