using FluentValidation;
using Project.Application.Features.Views.CreateView;

namespace Project.API.Endpoint.Views.CreateView
{
    public class CreateViewValidator : AbstractValidator<CreateViewRequest>
    {
        public CreateViewValidator()
        {
            RuleFor(x => x.FileId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("FileId phải là số không âm.");

            // Rule cho Name
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống.")
                .MaximumLength(100)
                .WithMessage("Tên không được dài quá 100 ký tự.");

            // Rule cho Description
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Mô tả không được dài quá 500 ký tự.");
        }
    }
}
