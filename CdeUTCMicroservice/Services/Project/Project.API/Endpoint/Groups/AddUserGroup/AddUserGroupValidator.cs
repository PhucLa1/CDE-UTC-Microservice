using FluentValidation;
using Project.Application.Features.Groups.AddUserGroup;

namespace Project.API.Endpoint.Groups.AddUserGroup
{
    public class AddUserGroupValidator : AbstractValidator<AddUserGroupRequest>
    {
        public AddUserGroupValidator()
        {
            RuleFor(x => x.UserIds)
               .NotEmpty().WithMessage("Danh sách UserIds không được để trống.")
               .Must(userIds => userIds.All(id => id > 0))
               .WithMessage("Tất cả UserIds phải là số nguyên dương.");

            RuleFor(x => x.GroupId)
                .GreaterThan(0).WithMessage("GroupId phải là số nguyên dương.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId phải là số nguyên dương.");
        }
    }
}
