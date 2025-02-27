using FluentValidation;
using Project.Application.Features.Comment.ViewComments.CreateViewComment;

namespace Project.API.Endpoint.Comment.ViewComments.CreateViewComment
{
    public class CreateViewCommentValidator : AbstractValidator<CreateViewCommentRequest>
    {
        public CreateViewCommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Nội dung không được để trống");

            RuleFor(x => x.ViewId)
                .GreaterThan(0)
                .WithMessage("View Id phải lớn hơn 0");
        }
    }
}
