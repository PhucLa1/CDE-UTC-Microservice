using Auth.Application.Auth.SignUp;

namespace Auth.API.Controllers.Auth.SignUp
{
    public class SignUpValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email không được bỏ trống")
                .EmailAddress()
                .WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Mật khẩu không được để trống");
            RuleFor(x => x.RePassword)
                .NotEmpty()
                .WithMessage("Nhập lại mật khẩu không được để trống")
                .Equal(x => x.Password)
                .WithMessage("Nhập lại mật khẩu không khớp với mật khẩu.");
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Họ không được để trống");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
            RuleFor(x => x.MobilePhonenumber)
                .NotEmpty()
                .WithMessage("Số điện thoại không được để trống")
                .Matches(@"^\d{10}$")
                .WithMessage("Số điện thoại phải là 10 chữ số và chỉ chứa số.");

        }
    }
}
