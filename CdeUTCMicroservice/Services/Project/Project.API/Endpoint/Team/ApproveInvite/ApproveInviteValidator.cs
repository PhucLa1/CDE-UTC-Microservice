using FluentValidation;
using Project.Application.Features.Team.ApproveInvite;

namespace Project.API.Endpoint.Team.ApproveInvite
{
    public class ApproveInviteValidator : AbstractValidator<ApproveInviteRequest>
    {
        public ApproveInviteValidator()
        {
            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage("User id không được để trống");

            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage("Project Id không được để trống");
        }
    }
}
