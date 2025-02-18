using FluentValidation;
using Project.Application.Features.Groups.DeleteUserGroup;

namespace Project.API.Endpoint.Groups.DeleteUserGroup
{
    public class DeleteUserGroupValidator : AbstractValidator<DeleteUserGroupRequest>
    {
        public DeleteUserGroupValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId phải lớn hơn 0.");

            RuleFor(x => x.GroupId)
                .GreaterThan(0).WithMessage("GroupId phải lớn hơn 0.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId phải lớn hơn 0.");
        }
    }
}
