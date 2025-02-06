using FluentValidation;
using Project.Application.Features.Statuses.CreateStatus;

namespace Project.API.Endpoint.Statuses.CreateStatus
{
    public class CreateStatusValidator : AbstractValidator<CreateStatusRequest>
    {
        public CreateStatusValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage("Project Id không được để trống");
            RuleFor(x => x.ColorRGB).NotEmpty()
                .WithMessage("Không được để trống mã màu");
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Tên trạng thái không được để trống");
            RuleFor(x => x.IsDefault).NotEmpty()
                .WithMessage("Mặc định không được để trống");
        }
    }
}
