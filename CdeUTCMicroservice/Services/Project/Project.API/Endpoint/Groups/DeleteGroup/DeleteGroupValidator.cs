using FluentValidation;
using Project.Application.Features.Groups.DeleteGroup;

namespace Project.API.Endpoint.Groups.DeleteGroup
{
    public class DeleteGroupValidator : AbstractValidator<DeleteGroupRequest>
    {
        public DeleteGroupValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage("Project Id không được để trống");

            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("Id không được để trống");
        }
    }
}
