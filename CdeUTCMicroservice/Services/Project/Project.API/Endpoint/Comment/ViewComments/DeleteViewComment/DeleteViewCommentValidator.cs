using FluentValidation;
using Project.Application.Features.Comment.ViewComments.DeleteViewComment;

namespace Project.API.Endpoint.Comment.ViewComments.DeleteViewComment
{
    public class DeleteViewCommentValidator : AbstractValidator<DeleteViewCommentRequest>
    {
        public DeleteViewCommentValidator()
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
