using FluentValidation;
using Project.Application.Features.Comment.FolderComments.DeleteFolderComment;

namespace Project.API.Endpoint.Comment.FolderComments.DeleteFolderComment
{
    public class DeleteFolderCommentValidator : AbstractValidator<DeleteFolderCommentRequest>
    {
        public DeleteFolderCommentValidator() 
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
