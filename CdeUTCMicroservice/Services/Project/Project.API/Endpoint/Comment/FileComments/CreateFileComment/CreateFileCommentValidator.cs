using FluentValidation;
using Project.Application.Features.Comment.FileComments.CreateFileComment;

namespace Project.API.Endpoint.Comment.FileComments.CreateFileComment
{
    public class CreateFileCommentValidator : AbstractValidator<CreateFileCommentRequest>
    {
        public CreateFileCommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Nội dung không được để trống");

            RuleFor(x => x.FileId)
                .GreaterThan(0)
                .WithMessage("File Id phải lớn hơn 0");
        }
    }
}
