
namespace Project.Application.Features.Storage.GetFullPath
{
    public class GetFullPathHandler
        (IBaseRepository<Folder> folderRepository)
        : IQueryHandler<GetFullPathRequest, ApiResponse<List<GetFullPathResponse>>>
    {
        public async Task<ApiResponse<List<GetFullPathResponse>>> Handle(GetFullPathRequest request, CancellationToken cancellationToken)
        {
            var result = new List<GetFullPathResponse>() 
            { 
                new GetFullPathResponse()
                {
                    Name = "Lưu trữ dự án",
                    FolderId = 0
                }
            };
            if(request.FolderId is not 0)
            {
                var folderPath = await folderRepository.GetAllQueryAble()
                .Where(e => e.Id == request.FolderId)
                .Select(e => new
                {
                    FolderId = e.FullPath,
                    Name = e.FullPathName
                })
                .FirstOrDefaultAsync(cancellationToken);

                if (folderPath is null)
                    throw new NotFoundException(Message.NOT_FOUND);
                var ids = folderPath.FolderId.Split("/").Skip(1);
                var names = folderPath.Name.Split("/").Skip(1);

                var addPath = ids.Zip(names, (id, name) => new GetFullPathResponse
                {
                    FolderId = int.TryParse(id, out var folderId) ? folderId : 0, // Tránh lỗi FormatException
                    Name = name
                }).ToList();

                result = result.Concat(addPath).ToList();
            }

            return new ApiResponse<List<GetFullPathResponse>> { Data = result, Message = Message.GET_SUCCESSFULLY };
            

        }
    }
}
