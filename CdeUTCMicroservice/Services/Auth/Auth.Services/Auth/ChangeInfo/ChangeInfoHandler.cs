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

            if (request.FirstName is not null) user.FirstName = request.FirstName;
            if (request.LastName is not null) user.LastName = request.LastName;
            if (request.MobilePhoneNumber is not null) user.MobilePhoneNumber = request.MobilePhoneNumber;
            if (request.WorkPhoneNumber is not null) user.WorkPhoneNumber = request.WorkPhoneNumber;
            if (request.CityId is not null) user.CityId = request.CityId.Value;
            if (request.DistrictId is not null) user.DistrictId = request.DistrictId.Value;
            if (request.WardId is not null) user.WardId = request.WardId.Value;
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
