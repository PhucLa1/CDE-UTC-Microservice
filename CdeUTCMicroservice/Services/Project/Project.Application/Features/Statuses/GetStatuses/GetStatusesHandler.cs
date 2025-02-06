namespace Project.Application.Features.Statuses.GetStatuses
{
    public class GetStatusesHandler
        (IBaseRepository<Status> statusRepository)
        : IQueryHandler<GetStatusesRequest, ApiResponse<List<GetStatusesResponse>>>
    {
        public async Task<ApiResponse<List<GetStatusesResponse>>> Handle(GetStatusesRequest request, CancellationToken cancellationToken)
        {
            var types = await statusRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == ProjectId.Of(request.ProjectId))
                .Select(e => new GetStatusesResponse
                {
                    Name = e.Name,
                    ColorRGB = e.ColorRGB,
                    IsDefault = e.IsDefault,
                    Id = e.Id.Value,
                    IsBlock = e.IsBlock
                })
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<GetStatusesResponse>> { Data = types, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
