using FluentValidation;
using Project.Application.Features.Comment.ViewComments.UpdateViewComment;

namespace Project.API.Endpoint.Comment.ViewComments.UpdateViewComment
{
    public class UpdateViewCommentValidator : AbstractValidator<UpdateViewCommentRequest>
    {
        public UpdateViewCommentValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id không được bé hơn 0");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("ProjectId không được bé hơn 0");

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id không được bé hơn 0");
        }
    }
}
