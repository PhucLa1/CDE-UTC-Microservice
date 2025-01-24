using Auth.Application.Auth.SendEmailVerify;

namespace Auth.API.Controllers.Auth.SendEmailVerify
{
    public class SendEmailVerifyValidator : AbstractValidator<SendEmailVerifyRequest>
    {
        public SendEmailVerifyValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Email không được bỏ trống")
               .EmailAddress()
               .WithMessage("Email không đúng định dạng");
        }
    }
}
