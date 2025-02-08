using FluentValidation;
using Project.Application.Features.Priorities.DeletePriority;

namespace Project.API.Endpoint.Priorities.DeletePriority
{
    public class DeletePriorityValidator : AbstractValidator<DeletePriorityRequest>
    {
        public DeletePriorityValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id không được để trống");
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Project Id không được để trống");
        }
    }
}
