

namespace Event.Features.Features.Activities.ActivityTypeParents.CreateActivityTypeParent
{
    public class CreateActivityTypeParentValidator : AbstractValidator<CreateActivityTypeParentRequest>
    {
        public CreateActivityTypeParentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }
}
