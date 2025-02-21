using FluentValidation;
using Project.Application.Features.Storage.UpdateFile;

namespace Project.API.Endpoint.Storage.UpdateFile
{
    public class UpdateFileValidator : AbstractValidator<UpdateFileRequest>
    {
        public UpdateFileValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id phải lớn hơn 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("ProjectId phải lớn hơn 0 nếu được cung cấp.");

            RuleForEach(x => x.TagIds)
                .GreaterThanOrEqualTo(0)
                .WithMessage("TagIds không được chứa giá trị âm.");
        }
    }
}
