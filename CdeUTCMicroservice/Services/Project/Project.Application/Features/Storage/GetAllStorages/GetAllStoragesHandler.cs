using Project.Application.Extensions;
using Project.Application.Grpc;
using Project.Application.Grpc.GrpcRequest;

namespace Project.Application.Features.Storage.GetAllStorages
{
    public class GetAllStoragesHandler
        (IBaseRepository<Folder> folderRepository,
        IBaseRepository<File> fileRepository,
        IUserGrpc userGrpc)
        : IQueryHandler<GetAllStoragesRequest, ApiResponse<List<GetAllStoragesResponse>>>
    {
        public async Task<ApiResponse<List<GetAllStoragesResponse>>> Handle(GetAllStoragesRequest request, CancellationToken cancellationToken)
        {
            var IMAGE_EXTENSION = new List<string>() { ".png", ".jpg", ".jpeg" };
            //lấy định dạng ngày tháng
            var currentDateDisplay = folderRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = folderRepository.GetCurrentTimeDisplay();

            var folders = await folderRepository.GetAllQueryAble()
                .Include(e => e.FolderTags)
                .ThenInclude(e => e.Tag)
                .Where(e => e.ProjectId == request.ProjectId && e.ParentId == request.ParentId)
                .Select(e => new GetAllStoragesResponse()
                {
                    Id = e.Id,
                    IsFile = false,
                    Name = e.Name,
                    UrlImage = "",
                    CreatedAt = e.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    CreatedBy = e.CreatedBy,
                    TagNames = e.FolderTags.Select(e => e.Tag.Name).ToList()
                })
                .ToListAsync(cancellationToken);

            var files = await fileRepository.GetAllQueryAble()
                .Include(e => e.FileTags)
                .ThenInclude(e => e.Tag)
                .Where(e => e.ProjectId == request.ProjectId && e.FolderId == request.ParentId)
                .Select(e => new GetAllStoragesResponse()
                {
                    Id = e.Id,
                    IsFile = true,
                    Name = e.Name + e.Extension,
                    UrlImage = IMAGE_EXTENSION.Contains(e.Extension)
                    ? e.Url
                    : Setting.PROJECT_HOST + "/Extension/" + e.Extension.ConvertToUrl(),
                    CreatedAt = e.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                    CreatedBy = e.CreatedBy,
                    TagNames = e.FileTags.Select(e => e.Tag.Name).ToList()
                })
                .ToListAsync(cancellationToken);

            var folderCreatedByList = folders.Select(e => e.CreatedBy).ToList();
            var fileCreatedByList = files.Select(e => e.CreatedBy).ToList();
            var mergeList = folderCreatedByList.Concat(fileCreatedByList).Distinct().ToList();

            var users = await userGrpc
                .GetUsersByIds(new GetUserRequestGrpc { Ids = mergeList });

            var storage = files.Concat(folders).ToList();

            var storages = (from s in storage
                            join u in users on s.CreatedBy equals u.Id
                            select new GetAllStoragesResponse()
                            {
                                Id = s.Id,
                                IsFile = s.IsFile,
                                Name = s.Name,
                                UrlImage = s.UrlImage,
                                CreatedAt = s.CreatedAt,
                                CreatedBy = s.CreatedBy,
                                NameCreatedBy = u.FullName,
                                TagNames = ConvertTagsToView(s.TagNames)
                            }).ToList();

            return new ApiResponse<List<GetAllStoragesResponse>> { Data = storages, Message = Message.GET_SUCCESSFULLY };
        }

        private List<string> ConvertTagsToView(List<string> tagNames)
        {
            var MAX_COUNT = 25;
            List<string> result = new List<string>();
            var countChars = 0;
            for (int i = 0; i < tagNames.Count; i++)
            {
                countChars += tagNames[i].Count();
                if (countChars <= MAX_COUNT)
                {
                    result.Add(tagNames[i]);
                }
                else
                {
                    result.Add("+ " + (tagNames.Count() - i).ToString());
                    break;
                }
            }

            return result;
        }
    }
}
