namespace Event.Features.Features.Activities.ActivityTypeParents.UpdateActivityTypeParent
{
    public class UpdateActivityTypeParentValidator : AbstractValidator<UpdateActivityTypeParentRequest>
    {
        public UpdateActivityTypeParentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }
}
