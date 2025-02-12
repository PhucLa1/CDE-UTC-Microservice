using FluentValidation;
using Project.Application.Features.Groups.CreateGroup;

namespace Project.API.Endpoint.Groups.CreateGroup
{
    public class CreateGroupValidator : AbstractValidator<CreateGroupRequest>
    {
        public CreateGroupValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("Project Id không được để trống");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }
}
