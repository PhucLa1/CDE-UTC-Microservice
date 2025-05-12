
using Event.Infrastructure.Grpc;
using Event.Infrastructure.Grpc.GrpcRequest;

namespace Event.Features.Features.Activities
{
    public class GetActivitiesHandler
        (IBaseRepository<Activity> activityRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetActivitiesRequest, ApiResponse<List<GetActivitiesResponse>>>
    {
        public async Task<ApiResponse<List<GetActivitiesResponse>>> Handle(GetActivitiesRequest request, CancellationToken cancellationToken)
        {

            var activities = await activityRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == request.ProjectId).ToListAsync(cancellationToken);

            //Filter by TYPE
            if(request.TypeActivities != null && request.TypeActivities.Any())
            {
                activities = activities.Where(e => request.TypeActivities.Contains(e.TypeActivity)).ToList();
            }

            //Filter By CreatedBy
            if(request.CreatedBys != null  && request.CreatedBys.Any())
            {
                activities = activities.Where(e => request.CreatedBys.Contains(e.CreatedBy)).ToList();
            }

            //Filter By StartDate and EndDate
            if (request.StartDate != null && request.EndDate != null)
            {
                activities = activities.Where(e => e.CreatedAt > request.StartDate && e.CreatedAt < request.EndDate).ToList();
            }

            var createdBys = activities.Select(e => e.CreatedBy).Distinct().ToList();
            var usersList = await userGrpc
              .GetUsersByIds(new GetUserRequestGrpc { Ids = createdBys });

            var result = (from a in activities 
                               join u in usersList on a.CreatedBy equals u.Id
                               orderby a.Id descending
                               select new GetActivitiesResponse()
                               {
                                   Id = a.Id,
                                   Action = a.Action,
                                   ResourceId = a.ResourceId,
                                   Content = a.Content,
                                   TypeActivity = a.TypeActivity,
                                   ProjectId = a.ProjectId,
                                   CreatedAt = a.CreatedAt.ToString(),
                                   FullName = u.FullName,
                                   UserId = u.Id,
                                   Email = u.Email,
                                   ImageUrl = u.ImageUrl,
                               }).ToList();

            return new ApiResponse<List<GetActivitiesResponse>> { Data = result, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
