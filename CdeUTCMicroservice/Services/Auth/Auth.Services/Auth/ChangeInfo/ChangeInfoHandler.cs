using BuildingBlocks;
using BuildingBlocks.Extensions;

namespace Auth.Application.Auth.ChangeInfo
{
    public class ChangeInfoHandler
        (IBaseRepository<User> userRepository)
        : ICommandHandler<ChangeInfoRequest, ChangeInfoResponse>
    {
        public async Task<ChangeInfoResponse> Handle(ChangeInfoRequest request, CancellationToken cancellationToken)
        {
            var idCurrent = userRepository.GetDbContext.GetCurrentUserId();
            var user = await userRepository.GetAllQueryAble()
                            .FirstOrDefaultAsync(x => x.Id == idCurrent);

            if (user is null)
                throw new NotFoundException(Message.NOT_FOUND);

            if (request.Email is not null) user.Email = request.Email;
            if (request.MobilePhoneNumber is not null) user.MobilePhoneNumber = request.MobilePhoneNumber;
            if (request.WorkPhoneNumber is not null) user.WorkPhoneNumber = request.WorkPhoneNumber;
            if (request.LanguageId is not null) user.LanguageId = request.LanguageId;
            if (request.CityId is not null) user.CityId = request.CityId;
            if (request.DateDisplay is not null) user.DateDisplay = request.DateDisplay.Value;
            if (request.TimeDisplay is not null) user.TimeDisplay = request.TimeDisplay.Value;
            if (request.Employer is not null) user.Employer = request.Employer;
            if (request.JobTitleId is not null) user.JobTitleId = request.JobTitleId;
            if (request.Image is not null) user.ImageUrl = HandleFile.UPLOAD("User", request.Image);

            //Save
            userRepository.Update(user);
            await userRepository.SaveChangeAsync(cancellationToken);
            return new ChangeInfoResponse() { Message = Message.UPDATE_SUCCESSFULLY, Data = true };
        }
    }
}
