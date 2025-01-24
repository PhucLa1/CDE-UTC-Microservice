using Auth.Application.Auth.VerifyCode;

namespace Auth.API.Controllers.Auth.VerifyCode
{
    public class VerifyCodeValidator : AbstractValidator<VerifyCodeRequest>
    { 
        public VerifyCodeValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống.")
                .EmailAddress().WithMessage("Email không hợp lệ.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code không được để trống.")
                .Length(6).WithMessage("Code phải đúng 6 ký tự.")
                .Matches(@"^\d{6}$").WithMessage("Code chỉ được phép chứa các số.");
        }
    }
}
