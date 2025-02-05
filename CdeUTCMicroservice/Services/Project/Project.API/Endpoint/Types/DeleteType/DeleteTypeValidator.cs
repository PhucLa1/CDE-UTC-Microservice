using FluentValidation;
using Project.Application.Features.Types.DeleteType;

namespace Project.API.Endpoint.Types.DeleteType
{
    public class DeleteTypeValidator : AbstractValidator<DeleteTypeRequest>
    {
        public DeleteTypeValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Project Id không được để trống");
        }
    }
}
