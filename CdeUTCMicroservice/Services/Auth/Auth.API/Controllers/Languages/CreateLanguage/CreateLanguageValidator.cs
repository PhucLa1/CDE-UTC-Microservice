using Auth.Application.Languages.CreateLanguages;

namespace Auth.API.Controllers.Languages.CreateLanguage
{
    public class CreateLanguageValidator : AbstractValidator<CreateLanguageRequest>
    {
        public CreateLanguageValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }
}
