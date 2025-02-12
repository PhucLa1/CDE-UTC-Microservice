
namespace Project.Application.Features.Groups.GetGroupsByProjectId
{
    public class GetGroupsByProjectIdHandler
        (IBaseRepository<Group> groupRepository)
        : ICommandHandler<GetGroupsByProjectIdRequest, ApiResponse<List<GetGroupsByProjectIdResponse>>>
    {
        public async Task<ApiResponse<List<GetGroupsByProjectIdResponse>>> Handle(GetGroupsByProjectIdRequest request, CancellationToken cancellationToken)
        {
            var groups = await groupRepository.GetAllQueryAble()
                .Include(e => e.UserGroups)
                .Where(e => e.ProjectId == request.ProjectId)
                .Select(e => new GetGroupsByProjectIdResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    UserCount = e.UserGroups == null ? 0 : e.UserGroups.Count()
                })
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<GetGroupsByProjectIdResponse>> { Data = groups, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
