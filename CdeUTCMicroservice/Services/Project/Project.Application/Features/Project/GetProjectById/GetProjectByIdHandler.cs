
using BuildingBlocks.Exceptions;

namespace Project.Application.Features.Project.GetProjectById
{
    public class GetProjectByIdHandler
        (
        IBaseRepository<ProjectEntity> projectEntityRepository,
        IBaseRepository<File> fileRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IBaseRepository<Folder> folderRepository
        )
        : IQueryHandler<GetProjectByIdRequest, ApiResponse<GetProjectByIdResponse>>
    {
        public async Task<ApiResponse<GetProjectByIdResponse>> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
        {
            //Lấy ra kiểu ngày tháng mà người dùng hiện tại
            var currentDateDisplay = userProjectRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = userProjectRepository.GetCurrentTimeDisplay();

            var userCount = await userProjectRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == request.Id && e.UserProjectStatus == UserProjectStatus.Active)
                .CountAsync(cancellationToken);

            var project = await projectEntityRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (project is null)
                throw new NotFoundException(Message.NOT_FOUND);


            var getProjectByIdResponse = new GetProjectByIdResponse()
            {
                Name = project.Name,
                ImageUrl = Setting.PROJECT_HOST + "/Project/" + project.ImageUrl,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Description = project.Description,
                Ownership = project.Ownership,
                UserCount = userCount,
                FolderCount = fileRepository.GetAllQueryAble().Where(e => e.ProjectId == request.Id).Count(),
                FileCount = folderRepository.GetAllQueryAble().Where(e => e.ProjectId == request.Id).Count(),
                Size = (double)fileRepository.GetAllQueryAble().Where(e => e.ProjectId == request.Id).Sum(e => e.Size),
                CreatedAt = project.CreatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                UpdatedAt = project.UpdatedAt.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
            };

            return new ApiResponse<GetProjectByIdResponse> { Data = getProjectByIdResponse, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
