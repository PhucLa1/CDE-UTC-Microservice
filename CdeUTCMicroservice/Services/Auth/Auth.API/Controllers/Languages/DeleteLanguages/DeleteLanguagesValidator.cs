using Auth.Application.Languages.DeleteLanguages;

namespace Auth.API.Controllers.Languages.DeleteLanguages
{
    public class DeleteLanguagesValidator : AbstractValidator<DeleteLanguagesRequest>
    {
        public DeleteLanguagesValidator()
        {
            RuleFor(x => x.LanguageIds)
                .NotNull()
                .WithMessage("LanguageIds không được null!")
                .NotEmpty()
                .WithMessage("LanguageIds không được để trống");
        }
    }
}
