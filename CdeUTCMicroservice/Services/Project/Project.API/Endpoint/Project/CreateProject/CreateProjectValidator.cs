using FluentValidation;
using Project.Application.Features.Project.CreateProject;

namespace Project.API.Endpoint.Project.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên dự án không được để trống.")
                .Length(1, 100).WithMessage("Tên dự án phải từ 1 đến 100 ký tự.");

            // Kiểm tra ImageUrl (không rỗng, phải là URL hợp lệ)
            RuleFor(x => x.Image)
                .NotNull().WithMessage("Ảnh không được để trống.")
                .Must(IsValidImage).WithMessage("Ảnh phải là file hợp lệ (jpg, jpeg, png).");

            // Kiểm tra StartDate (không rỗng, phải là ngày trong tương lai)
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Ngày bắt đầu không được để trống.")
                .GreaterThan(DateTime.Now).WithMessage("Ngày bắt đầu phải lớn hơn ngày hiện tại.");

            // Kiểm tra EndDate (không rỗng, phải lớn hơn StartDate)
            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Ngày kết thúc không được để trống.")
                .GreaterThan(x => x.StartDate).WithMessage("Ngày kết thúc phải lớn hơn ngày bắt đầu.");

            // Kiểm tra Description (không rỗng, độ dài từ 10 đến 1000 ký tự)
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Mô tả dự án không được để trống.")
                .Length(10, 1000).WithMessage("Mô tả phải từ 10 đến 1000 ký tự.");
        }
        private bool IsValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
            return file != null && allowedExtensions.Contains(fileExtension);
        }
    }
}
