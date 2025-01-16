using Auth.Application.Languages.CreateLanguage;

namespace Auth.Application.Languages.CreateLanguages
{
    public class CreateLanguageRequest : ICommand<CreateLanguageResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}
