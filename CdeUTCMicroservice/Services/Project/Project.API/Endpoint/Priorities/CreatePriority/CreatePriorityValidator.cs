using FluentValidation;
using Project.Application.Features.Priorities.CreatePriority;

namespace Project.API.Endpoint.Priorities.CreatePriority
{
    public class CreatePriorityValidator : AbstractValidator<CreatePriorityRequest>
    {
        public CreatePriorityValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Tên không được để trống");
            RuleFor(x => x.ColorRGB).NotEmpty()
                .WithMessage("Màu không được để trống");
            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage("Project Id không được để trống");
        }
    }
}
