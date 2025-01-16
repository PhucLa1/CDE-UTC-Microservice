
using Auth.Application.Languages.CreateLanguages;
using Mapster;

namespace Auth.Application.Languages.CreateLanguage
{
    public class CreateLanguageHandler
        (IBaseRepository<Language> languageRepository)
        : ICommandHandler<CreateLanguageRequest, CreateLanguageResponse>
    {
        public async Task<CreateLanguageResponse> Handle(CreateLanguageRequest request, CancellationToken cancellationToken)
        {
            var language = request.Adapt<Language>();
            await languageRepository.AddAsync(language, cancellationToken);
            await languageRepository.SaveChangeAsync(cancellationToken);
            return new CreateLanguageResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
