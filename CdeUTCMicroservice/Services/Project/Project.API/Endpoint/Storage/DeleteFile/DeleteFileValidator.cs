using FluentValidation;
using Project.Application.Features.Storage.DeleteFile;

namespace Project.API.Endpoint.Storage.DeleteFile
{
    public class DeleteFileValidator : AbstractValidator<DeleteFileRequest>
    {
        public DeleteFileValidator()
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
