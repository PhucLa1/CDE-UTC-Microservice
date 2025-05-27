
namespace Auth.Application.Auth.GetUserByIds
{
    public class GetUserByIdsHandler
        (IBaseRepository<User> userRepository)
        : IQueryHandler<GetUserByIdsRequest, ApiResponse<List<GetUserByIdsResponse>>>
    {
        public async Task<ApiResponse<List<GetUserByIdsResponse>>> Handle(GetUserByIdsRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAllQueryAble()
                .Where(e => request.Ids.Contains(e.Id))
                .Select(e => new GetUserByIdsResponse
                {
                    FullName = e.FirstName + " " + e.LastName,
                    Id = e.Id,
                    Email = e.Email,
                    ImageUrl = Setting.AUTH_HOST + "/User/" + e.ImageUrl,
                })
                .ToListAsync(cancellationToken);
            Console.WriteLine(user);

            return new ApiResponse<List<GetUserByIdsResponse>> { Data = user, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
