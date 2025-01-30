
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
            var userCount = await userProjectRepository.GetAllQueryAble()
                .Where(e => e.ProjectId == ProjectId.Of(request.Id))
                .CountAsync(cancellationToken);

            var project = await projectEntityRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == ProjectId.Of(request.Id));

            if (project is null)
                throw new NotFoundException(Message.NOT_FOUND);


            var getProjectByIdResponse = new GetProjectByIdResponse()
            {
                Name = project.Name,
                ImageUrl = "https://localhost:5052/Project/" + project.ImageUrl,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Description = project.Description,
                Ownership = project.Ownership,
                UserCount = userCount,
                FolderCount = 0,
                FileCount = 0,
                Size = 0.0,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
            };

            return new ApiResponse<GetProjectByIdResponse> { Data = getProjectByIdResponse, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
