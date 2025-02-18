using FluentValidation;
using Project.Application.Features.Storage.DeleteFolder;

namespace Project.API.Endpoint.Storage.DeleteFolder
{
    public class DeleteFolderValidator : AbstractValidator<DeleteFolderRequest>
    {
        public DeleteFolderValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id phải lớn hơn 0");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("Project Id phải lớn hơn 0");
        }
    }
}
