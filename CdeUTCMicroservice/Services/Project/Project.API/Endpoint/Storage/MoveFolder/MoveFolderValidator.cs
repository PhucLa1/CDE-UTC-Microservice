using FluentValidation;
using Project.Application.Features.Storage.MoveFolder;

namespace Project.API.Endpoint.Storage.MoveFolder
{
    public class MoveFolderValidator : AbstractValidator<MoveFolderRequest>
    {
        public MoveFolderValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");

            RuleFor(x => x.ParentId)
                .GreaterThanOrEqualTo(0).WithMessage("ParentId phải lớn hơn 0.");
        }
    }
}
