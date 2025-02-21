namespace Project.Application.Storage.GetFileById
{
    public class GetFileByIdRequest : IQuery<ApiResponse<GetFileByIdResponse>>
    {
        public int Id { get; set; }
    }
}
