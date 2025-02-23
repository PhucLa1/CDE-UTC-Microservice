using FluentValidation;
using Project.Application.Features.Storage.GetAllFolderDestination;

namespace Project.API.Endpoint.Storage.GetAllFolderDestination
{
    public class GetAllFolderDestinationValidator : AbstractValidator<GetAllFolderDestinationRequest>
    {
        public GetAllFolderDestinationValidator() 
        {
            RuleFor(x => x.FileIds)
                .NotNull().WithMessage("FileIds không được null.");

            RuleFor(x => x.FolderIds)
                .NotNull().WithMessage("FolderIds không được null.");

            RuleFor(x => x.ParentId)
                .GreaterThanOrEqualTo(0).WithMessage("ParentId phải lớn hơn 0.");
        }
    }
}
