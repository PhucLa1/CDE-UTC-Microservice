using FluentValidation;
using Project.Application.Features.Priorities.UpdatePriority;

namespace Project.API.Endpoint.Priorities.UpdatePriority
{
    public class UpdatePriorityValidator : AbstractValidator<UpdatePriorityRequest>
    {
        public UpdatePriorityValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("ProjectId không được để trống.");

            RuleFor(x => x.ColorRGB)
                .NotEmpty().WithMessage("Màu không không được để trống.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(100).WithMessage("Tên không được vượt quá 100 ký tự.");
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
        }
    }
}
