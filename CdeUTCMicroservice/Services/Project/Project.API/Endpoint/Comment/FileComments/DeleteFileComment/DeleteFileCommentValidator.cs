using FluentValidation;
using Project.Application.Features.Comment.FileComments.DeleteFileComment;

namespace Project.API.Endpoint.Comment.FileComments.DeleteFileComment
{
    public class DeleteFileCommentValidator : AbstractValidator<DeleteFileCommentRequest>
    {
        public DeleteFileCommentValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id không được bé hơn 0");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("ProjectId không được bé hơn 0");
        }
    }
}
