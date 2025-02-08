namespace Project.Application.Features.Priorities.GetPriorities
{
    public class GetPrioritiesHandler
        (IBaseRepository<Priority> priorityRepository)
        : IQueryHandler<GetPrioritiesRequest, ApiResponse<List<GetPrioritiesResponse>>>
    {
        public async Task<ApiResponse<List<GetPrioritiesResponse>>> Handle(GetPrioritiesRequest request, CancellationToken cancellationToken)
        {
            var priorities = await priorityRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == request.ProjectId)
                .Select(e => new GetPrioritiesResponse
                {
                    Name = e.Name,
                    ColorRGB = e.ColorRGB,
                    Id = e.Id,
                    IsBlock = e.IsBlock
                })
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<GetPrioritiesResponse>> { Data = priorities, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
