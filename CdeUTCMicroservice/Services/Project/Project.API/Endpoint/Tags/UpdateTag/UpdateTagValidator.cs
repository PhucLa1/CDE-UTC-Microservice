using FluentValidation;
using Project.Application.Features.Tags.UpdateTag;

namespace Project.API.Endpoint.Tags.UpdateTag
{
    public class UpdateTagValidator : AbstractValidator<UpdateTagRequest>
    {
        public UpdateTagValidator()
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
