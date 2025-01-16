using MediatR;

namespace Auth.Application.Languages.DeleteLanguages
{
    public class DeleteLanguagesRequest : ICommand<DeleteLanguagesResponse>
    {
        public List<Guid> LanguageIds { get; set; } = new();
    }
}
