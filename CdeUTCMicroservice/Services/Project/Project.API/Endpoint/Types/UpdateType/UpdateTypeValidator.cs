using FluentValidation;
using Project.Application.Features.Types.UpdateType;

namespace Project.API.Endpoint.Types.UpdateType
{
    public class UpdateTypeValidator : AbstractValidator<UpdateTypeRequest>
    {
        public UpdateTypeValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("ProjectId không được để trống.");

            RuleFor(x => x.IconImage)
                .NotNull().WithMessage("IconImage không được để trống.")
                .Must(IsValidImage).WithMessage("IconImage phải là một file ảnh hợp lệ (jpg, jpeg, png).");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(100).WithMessage("Tên không được vượt quá 100 ký tự.");
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id không được để trống");
        }

        private bool IsValidImage(IFormFile file)
        {
            if (file == null) return false;
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }
}
