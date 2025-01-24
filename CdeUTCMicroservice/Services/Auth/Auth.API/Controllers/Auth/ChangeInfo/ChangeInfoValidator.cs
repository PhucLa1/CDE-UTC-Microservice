using Auth.Application.Auth.ChangeInfo;

namespace Auth.API.Controllers.Auth.ChangeInfo
{
    public class ChangeInfoValidator : AbstractValidator<ChangeInfoRequest>
    {
        public ChangeInfoValidator()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Họ không được để trống.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Tên không được để trống.");

            RuleFor(x => x.MobilePhoneNumber)
                .Matches(@"^\d{10}$").WithMessage("Sai định dạng số điện thoại.");

            RuleFor(x => x.WorkPhoneNumber)
                .Matches(@"^\d{10}$").WithMessage("Sai định dạng số điện thoại.");

            RuleFor(x => x.Employer)
                .MaximumLength(100).WithMessage("Employer name must not exceed 100 characters.");

            RuleFor(x => x.Image)
                .Must(file => file == null || IsValidImageFile(file)).WithMessage("Invalid image file format. Allowed formats: .jpg, .png, .jpeg.");

        }

        private bool IsValidImageFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }
}
