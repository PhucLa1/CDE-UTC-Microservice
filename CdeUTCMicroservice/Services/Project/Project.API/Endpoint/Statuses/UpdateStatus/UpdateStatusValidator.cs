using FluentValidation;
using Project.Application.Features.Statuses.UpdateStatus;

namespace Project.API.Endpoint.Statuses.UpdateStatus
{
    public class UpdateStatusValidator : AbstractValidator<UpdateStatusRequest>
    {
        public UpdateStatusValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty()
               .WithMessage("Project Id không được để trống");
            RuleFor(x => x.ColorRGB).NotEmpty()
                .WithMessage("Không được để trống mã màu");
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Tên trạng thái không được để trống");
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("Tên trạng thái không được để trống");
            RuleFor(x => x.IsDefault).NotEmpty()
                .WithMessage("Mặc định không được để trống");
        }
    }
}
