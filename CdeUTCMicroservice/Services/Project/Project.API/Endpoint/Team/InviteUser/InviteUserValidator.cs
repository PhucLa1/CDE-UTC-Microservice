using FluentValidation;
using Project.Application.Features.Team.InviteUser;
using Project.Domain.Enums;

namespace Project.API.Endpoint.Team.InviteUser
{
    public class InviteUserValidator : AbstractValidator<InviteUserRequest>
    {
        public InviteUserValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("Project ID không được để trống");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email không được để trống")
                .EmailAddress()
                .WithMessage("Email không đúng định dạng");

            RuleFor(x => x.Role)
                .NotNull()
                .WithMessage("Role không được để trống")
                .IsInEnum()
                .WithMessage("Role phải là giá trị trong enum");
        }
    }
}
