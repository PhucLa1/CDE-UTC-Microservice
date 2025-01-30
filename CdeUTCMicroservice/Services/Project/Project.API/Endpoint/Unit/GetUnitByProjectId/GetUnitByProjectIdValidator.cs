using FluentValidation;
using Project.Application.Features.Unit.GetUnitByProjectId;

namespace Project.API.Endpoint.Unit.GetUnitByProjectId
{
    public class GetUnitByProjectIdValidator : AbstractValidator<GetUnitByProjectIdRequest>
    {
        public GetUnitByProjectIdValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .WithMessage("Không được bỏ trống project id");
        }
    }
}
