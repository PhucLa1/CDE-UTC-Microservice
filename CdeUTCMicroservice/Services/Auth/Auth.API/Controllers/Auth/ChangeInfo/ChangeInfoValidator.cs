using Auth.Application.Auth.ChangeInfo;

namespace Auth.API.Controllers.Auth.ChangeInfo
{
    public class ChangeInfoValidator : AbstractValidator<ChangeInfoRequest>
    {
        public ChangeInfoValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.MobilePhoneNumber)
                .NotEmpty().WithMessage("Mobile phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid mobile phone number format.");

            RuleFor(x => x.WorkPhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid work phone number format.");

            RuleFor(x => x.Employer)
                .MaximumLength(100).WithMessage("Employer name must not exceed 100 characters.");

            RuleFor(x => x.Image)
                .Must(file => IsValidImageFile(file!)).WithMessage("Invalid image file format. Allowed formats: .jpg, .png, .jpeg.");
        }

        private bool IsValidImageFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }
}
