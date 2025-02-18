using FluentValidation;
using Project.Application.Features.Storage.CreateFolder;

namespace Project.API.Endpoint.Storage.CreateFolder
{
    public class CreateFolderValidator : AbstractValidator<CreateFolderRequest>
    {
        public CreateFolderValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên thư mục không được để trống.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("ProjectId phải lớn hơn 0.");

            RuleFor(x => x.ParentId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ParentId phải lớn hơn hoặc bằng 0.");
        }
    }
}
