using FluentValidation;
using Project.Application.Features.Comment.FileComments.UpdateFileComment;

namespace Project.API.Endpoint.Comment.FileComments.UpdateFileComment
{
    public class UpdateFileCommentValidator : AbstractValidator<UpdateFileCommentRequest>
    {
        public UpdateFileCommentValidator()
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
