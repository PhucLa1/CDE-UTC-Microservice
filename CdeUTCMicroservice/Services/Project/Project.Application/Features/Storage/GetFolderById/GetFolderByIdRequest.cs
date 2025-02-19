namespace Project.Application.Features.Storage.GetFolderById
{
    public class GetFolderByIdRequest : IQuery<ApiResponse<GetFolderByIdResponse>>
    {
        public int Id { get; set; }
    }
}
