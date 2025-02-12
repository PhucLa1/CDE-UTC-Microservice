using FluentValidation;
using Project.Application.Features.Groups.UpdateGroup;

namespace Project.API.Endpoint.Groups.UpdateGroup
{
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupRequest>
    {
        public UpdateGroupValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage("Project Id không được để trống");

            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("Id không được để trống");

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }
}
