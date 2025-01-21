
namespace Event.Features.Features.Activities.ActivityTypeParents.CreateActivityTypeParent
{
    public class CreateActivityTypeParentRequest : ICommand<CreateActivityTypeParentResponse>
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile? IconImage { get; set; }
    }
}
