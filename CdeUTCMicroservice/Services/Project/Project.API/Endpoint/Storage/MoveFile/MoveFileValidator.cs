using FluentValidation;
using Project.Application.Features.Storage.MoveFile;

namespace Project.API.Endpoint.Storage.MoveFile
{
    public class MoveFileValidator : AbstractValidator<MoveFileRequest>
    {
        public MoveFileValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id phải lớn hơn 0.");

            RuleFor(x => x.FolderId)
                .GreaterThanOrEqualTo(0).WithMessage("FolderId phải lớn hơn 0.");
        }
    }
}
