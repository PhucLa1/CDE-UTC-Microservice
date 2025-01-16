using Auth.Application.Languages.UpdateLanguage;

namespace Auth.API.Controllers.Languages.UpdateLanguage
{
    public class UpdateLanguageValidator : AbstractValidator<UpdateLanguageRequest>
    {
        public UpdateLanguageValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id không được để trống");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }
}
