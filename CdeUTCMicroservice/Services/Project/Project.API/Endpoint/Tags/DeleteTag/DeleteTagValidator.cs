using FluentValidation;
using Project.Application.Features.Tags.DeleteTag;

namespace Project.API.Endpoint.Tags.DeleteTag
{
    public class DeleteTagValidator : AbstractValidator<DeleteTagRequest>
    {
        public DeleteTagValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithMessage("Không được để trống ids");
        }
    }
}
