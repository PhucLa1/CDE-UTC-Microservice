using FluentValidation;
using Project.Application.Features.Team.ChangeRole;

namespace Project.API.Endpoint.Team.ChangeRole
{
    public class ChangeRoleValidator : AbstractValidator<ChangeRoleRequest>
    {
        public ChangeRoleValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage("Project Id không được để trống");
            RuleFor(x => x.Role)
                .NotNull()
                .WithMessage("Role không được để trống")
                .IsInEnum()
                .WithMessage("Role phải là giá trị trong enum");
            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage("Project Id không được để trống");

        }
    }
}
