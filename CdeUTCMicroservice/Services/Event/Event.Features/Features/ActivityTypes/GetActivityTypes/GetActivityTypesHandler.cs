
using BuildingBlocks.Enums;

namespace Event.Features.Features.ActivityTypes.GetActivityTypes
{
    public class GetActivityTypesHandler
        (IBaseRepository<ActivityType> activityTypeRepository)
        : IQueryHandler<GetActivityTypesRequest, ApiResponse<List<GetActivityTypesReponse>>>
    {
        public async Task<ApiResponse<List<GetActivityTypesReponse>>> Handle(GetActivityTypesRequest request, CancellationToken cancellationToken)
        {
            var res = await activityTypeRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == request.ProjectId)
                .Select(e => new GetActivityTypesReponse()
                {
                    Id = e.Id,
                    TypeActivity = e.TypeActivity,
                    ProjectId = e.ProjectId,
                    IsAcceptAll = e.IsAcceptAll,
                    TimeSend = e.TimeSend,
                    Enabled = e.Enabled,
                })
                .ToListAsync(cancellationToken);
            return new ApiResponse<List<GetActivityTypesReponse>> { Data = res, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
