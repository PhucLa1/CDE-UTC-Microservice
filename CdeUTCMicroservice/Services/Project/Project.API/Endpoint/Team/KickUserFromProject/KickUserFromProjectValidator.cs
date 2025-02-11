using FluentValidation;
using Project.Application.Features.Team.KickUserFromProject;

namespace Project.API.Endpoint.Team.KickUserFromProject
{
    public class KickUserFromProjectValidator : AbstractValidator<KickUserFromProjectRequest>
    {
        public KickUserFromProjectValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User id không được để trống");

            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("Project Id không được để trống");
        }
    }
}
