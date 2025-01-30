namespace Project.Application.Features.Project.GetProjectById
{
    public class GetProjectByIdRequest : IQuery<ApiResponse<GetProjectByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
