using FluentValidation;
using Project.Application.Features.Views.UpdateView;

namespace Project.API.Endpoint.Views.UpdateView
{
    public class UpdateViewValidator : AbstractValidator<UpdateViewRequest>
    {
        public UpdateViewValidator() 
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
