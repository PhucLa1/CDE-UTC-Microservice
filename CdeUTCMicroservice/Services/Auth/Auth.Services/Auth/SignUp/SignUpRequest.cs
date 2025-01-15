namespace Auth.Application.Auth.SignUp
{
    public class SignUpRequest : ICommand<SignUpResponse>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string RePassword { get; set; } = default!;
        public string MobilePhonenumber { get; set; } = default!;

    }
}
