using FluentValidation;
using Project.Application.Features.Views.AddAnnotation;

namespace Project.API.Endpoint.Views.AddAnnotation
{
    public class AddAnnotationValidator : AbstractValidator<AddAnnotationRequest>
    {
        public AddAnnotationValidator()
        {
            RuleFor(x => x.Action)
                .IsInEnum()
                .WithMessage("Action không hợp lệ.");

            RuleFor(x => x.InkString)
                .NotEmpty()
                .WithMessage("InkString không được để trống.");

            RuleFor(x => x.ViewId)
                .GreaterThan(0)
                .WithMessage("ViewId phải lớn hơn 0.");
        }
    }
}
