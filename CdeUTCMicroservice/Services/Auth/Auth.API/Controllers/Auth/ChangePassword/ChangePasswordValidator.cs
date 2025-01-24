using Auth.Application.Auth.ChangePassword;

namespace Auth.API.Controllers.Auth.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống.")
                .EmailAddress().WithMessage("Email không hợp lệ.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống.")

            RuleFor(x => x.RePassword)
                .NotEmpty().WithMessage("Vui lòng nhập lại mật khẩu.")
                .Equal(x => x.Password).WithMessage("Mật khẩu nhập lại không khớp.");
        }
    }
}
