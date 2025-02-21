using FluentValidation;
using Project.Application.Features.Storage.CreateFile;

namespace Project.API.Endpoint.Storage.CreateFile
{
    public class CreateFileValidator : AbstractValidator<CreateFileRequest>
    {
        public CreateFileValidator() 
        {
            RuleFor(x => x.Name)
    .NotEmpty().WithMessage("Tên file không được để trống.")
    .MaximumLength(255).WithMessage("Tên file không được vượt quá 255 ký tự.");

            RuleFor(x => x.Size)
                .GreaterThan(0).WithMessage("Kích thước file phải lớn hơn 0.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL không được để trống.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("URL không hợp lệ.");

            RuleFor(x => x.MimeType)
                .NotEmpty().WithMessage("MIME Type không được để trống.")
                .MaximumLength(100).WithMessage("MIME Type không được vượt quá 100 ký tự.");

            RuleFor(x => x.Extension)
                .NotEmpty().WithMessage("Phần mở rộng không được để trống.")
                .Matches(@"^\.[a-zA-Z0-9]+$").WithMessage("Phần mở rộng file không hợp lệ."); // Ví dụ: .jpg, .pdf

            RuleFor(x => x.FolderId)
                .GreaterThan(-1).WithMessage("FolderId phải lớn hơn hoặc bằng 0.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId phải lớn hơn 0.");
        }
    }
}
