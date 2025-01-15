using Auth.Application.JobTitles.CreateJobTitle;

namespace Auth.API.Controllers.JobTitles.CreateJobTitle
{
    public class CreateJobTitleValidator : AbstractValidator<CreateJobTitleRequest>
    {
        public CreateJobTitleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }
}
