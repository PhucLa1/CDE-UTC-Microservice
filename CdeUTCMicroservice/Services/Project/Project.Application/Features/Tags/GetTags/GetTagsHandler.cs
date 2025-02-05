

using MassTransit.Initializers;

namespace Project.Application.Features.Tags.GetTags
{
    public class GetTagsHandler
        (IBaseRepository<Tag> tagRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : IQueryHandler<GetTagsRequest, ApiResponse<List<GetTagsResponse>>>
    {
        public async Task<ApiResponse<List<GetTagsResponse>>> Handle(GetTagsRequest request, CancellationToken cancellationToken)
        {
            //Lấy xem đang là role gì
            var currentUserId = userProjectRepository.GetCurrentId();
            var role = await userProjectRepository.GetAllQueryAble()
                .FirstAsync(e => e.ProjectId == ProjectId.Of(request.ProjectId) && e.UserId == currentUserId)
                .Select(e => e.Role);
            var isBlock = true;
            if (role == Role.Admin)
                isBlock = false;

            var tags = await tagRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == ProjectId.Of(request.ProjectId))
                .Select(e => new GetTagsResponse
                {
                    Id = e.Id.Value,
                    Name = e.Name,
                    IsBlock = isBlock,
                })
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<GetTagsResponse>> { Data = tags, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
