using FluentValidation;
using Project.Application.Features.Comment.FolderComments.CreateFolderComment;

namespace Project.API.Endpoint.Comment.FolderComments.CreateFolderComment
{
    public class CreateFolderCommentValidator : AbstractValidator<CreateFolderCommentRequest>
    {
        public CreateFolderCommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Nội dung không được để trống");

            RuleFor(x => x.FolderId)
                .GreaterThan(0)
                .WithMessage("Folder Id phải lớn hơn 0");
        }
    }
}
