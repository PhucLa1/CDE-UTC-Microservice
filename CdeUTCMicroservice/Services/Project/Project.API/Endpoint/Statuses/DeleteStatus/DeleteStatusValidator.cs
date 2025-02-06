using FluentValidation;
using Project.Application.Features.Statuses.DeleteStatus;

namespace Project.API.Endpoint.Statuses.DeleteStatus
{
    public class DeleteStatusValidator : AbstractValidator<DeleteStatusRequest>
    {
        public DeleteStatusValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("Id không được để trống");
            RuleFor(x => x.ProjectId).NotEmpty()
                .WithMessage("Id không được để trống");
        }
    }
}
