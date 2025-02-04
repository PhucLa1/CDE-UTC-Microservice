using FluentValidation;
using Project.Application.Features.Tags.CreateTag;

namespace Project.API.Endpoint.Tags.CreateTag
{
    public class CreateTagValidator : AbstractValidator<CreateTagRequest>
    {
        public CreateTagValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được bỏ trống");
            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("Project Id không được bỏ trống");
        }
    }
}
