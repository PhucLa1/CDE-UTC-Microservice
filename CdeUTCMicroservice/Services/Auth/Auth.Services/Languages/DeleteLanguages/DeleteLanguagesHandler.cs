
namespace Auth.Application.Languages.DeleteLanguages
{
    public class DeleteLanguagesHandler
        (IBaseRepository<Language> languageRepository)
        : ICommandHandler<DeleteLanguagesRequest, DeleteLanguagesResponse>
    {
        public async Task<DeleteLanguagesResponse> Handle(DeleteLanguagesRequest request, CancellationToken cancellationToken)
        {
            var languages = await languageRepository
                .GetAllQueryAble()
                .Where(e => request.LanguageIds.Contains(e.Id))
                .ToListAsync(cancellationToken);

            languageRepository.RemoveRange(languages);
            await languageRepository.SaveChangeAsync(cancellationToken);
            return new DeleteLanguagesResponse() { Data = true, Message = Message.DELETE_SUCCESSFULLY };
        }
    }
}
