namespace Project.Application.Features.Views.GetViewById
{
    public class GetViewByIdRequest : IQuery<ApiResponse<GetViewByIdResponse>>
    {
        public int Id { get; set; }
    }
}
