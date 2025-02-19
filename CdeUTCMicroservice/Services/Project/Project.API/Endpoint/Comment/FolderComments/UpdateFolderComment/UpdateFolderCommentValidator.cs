using FluentValidation;
using Project.Application.Features.Comment.FolderComments.UpdateFolderComment;

namespace Project.API.Endpoint.Comment.FolderComments.UpdateFolderComment
{
    public class UpdateFolderCommentValidator : AbstractValidator<UpdateFolderCommentRequest>
    {
        public UpdateFolderCommentValidator()
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
