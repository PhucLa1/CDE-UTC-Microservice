namespace Auth.API.Controllers.Auth.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Email không được bỏ trống")
               .EmailAddress()
               .WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Mật khẩu không được để trống");
        }
    }
}
