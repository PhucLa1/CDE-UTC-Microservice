namespace Project.Application.Features.Project.GetProjectById
{
    public class GetProjectByIdRequest : IQuery<ApiResponse<GetProjectByIdResponse>>
    {
        public int Id { get; set; }
    }
}
