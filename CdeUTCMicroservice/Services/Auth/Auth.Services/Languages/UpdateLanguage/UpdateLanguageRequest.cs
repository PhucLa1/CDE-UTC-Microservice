namespace Auth.Application.Languages.UpdateLanguage
{
    public class UpdateLanguageRequest : ICommand<UpdateLanguageResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
