using Auth.Application.JobTitles.DeletJobTitles;

namespace Auth.API.Controllers.JobTitles.DeleteJobTitles
{
    public class DeleteJobTitlesValidator : AbstractValidator<DeleteJobTitleRequest>
    {
        public DeleteJobTitlesValidator()
        {
            RuleFor(x => x.JobTitleIds)
                .NotNull()
                .WithMessage("JobTitleIds không được null!")
                .NotEmpty()
                .WithMessage("JobTitleIds không được để trống!");
        }
    }
}
