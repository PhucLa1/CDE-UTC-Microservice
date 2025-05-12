
namespace Event.Features.Features.ActivityTypes.UpdateActivityTypes
{
    public class UpdateActivityTypesHandler
        (IBaseRepository<ActivityType> activityTypeRepository)
        : ICommandHandler<UpdateActivityTypesRequest, UpdateActivityTypesResponse>
    {
        public async Task<UpdateActivityTypesResponse> Handle(UpdateActivityTypesRequest request, CancellationToken cancellationToken)
        {
            var updateDtos = request.UpdateActivityTypesDtos;
            var updateIds = updateDtos.Select(x => x.Id).ToList();

            var activityTypes = await activityTypeRepository
                .GetAllQueryAble()
                .Where(e => updateIds.Contains(e.Id) && e.ProjectId == request.ProjectId)
                .ToListAsync(cancellationToken);

            foreach (var activity in activityTypes)
            {
                var dto = updateDtos.FirstOrDefault(x => x.Id == activity.Id);
                if (dto is not null)
                {
                    activity.TimeSend = dto.TimeSend;
                    activity.Enabled = dto.Enabled;
                    activity.IsAcceptAll = request.IsAcceptAll;
                }
            }

            activityTypeRepository.UpdateMany(activityTypes); // nếu bạn có hàm UpdateRangeAsync
            await activityTypeRepository.SaveChangeAsync(cancellationToken);
            return new UpdateActivityTypesResponse();
        }

    }
}
