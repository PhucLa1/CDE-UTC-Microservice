namespace Auth.Application.Languages.UpdateLanguage
{
    public class UpdateLanguageHandler
        (IBaseRepository<Language> languageRepository)
        : ICommandHandler<UpdateLanguageRequest, UpdateLanguageResponse>
    {
        public async Task<UpdateLanguageResponse> Handle(UpdateLanguageRequest request, CancellationToken cancellationToken)
        {
            var language = await languageRepository
                .GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (language is null)
                throw new NotFoundException(Message.NOT_FOUND);

            language.Name = request.Name;
            languageRepository.Update(language);
            await languageRepository.SaveChangeAsync(cancellationToken);
            return new UpdateLanguageResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY }; ;
        }
    }
}
