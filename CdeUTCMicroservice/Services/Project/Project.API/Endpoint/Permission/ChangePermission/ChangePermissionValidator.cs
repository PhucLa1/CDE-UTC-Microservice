using FluentValidation;
using Project.Application.Features.Permission.ChangePermission;

namespace Project.API.Endpoint.Permission.ChangePermission
{
    public class ChangePermissionValidator : AbstractValidator<ChangePermissionRequest>
    {
        public ChangePermissionValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage("Project Id không được để trống");

            RuleFor(x => x.TodoVisibility)
                .IsInEnum()
                .WithMessage("TodoVisibility phải là giá trị trong enum");

            RuleFor(x => x.InvitationPermission)
                .IsInEnum()
                .WithMessage("InvitationPermission phải là giá trị trong enum");
        }
    }
}
