

using Project.Application.Dtos.Result;

namespace Project.Application.Features.Storage.GetFolderTree
{
    public class GetFolderTreeResponse
    {
        public string Name { get; set; } = string.Empty;
        public List<FileResult> Files { get; set; } = new List<FileResult>() { };
        public List<GetFolderTreeResponse> SubFolders { get; set; } = new List<GetFolderTreeResponse>() { };
    }
}
