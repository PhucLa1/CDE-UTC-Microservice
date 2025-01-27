


namespace Project.Application.Features.Project.GetProject
{
    public class GetProjectHandler
        (IBaseRepository<ProjectEntity> projectEntityRepository,
        IBaseRepository<UserProject> userProjectRepository)
        : IQueryHandler<GetProjectRequest, ApiResponse<List<GetProjectResponse>>>
    {
        public async Task<ApiResponse<List<GetProjectResponse>>> Handle(GetProjectRequest request, CancellationToken cancellationToken)
        {
            var idCurrent = projectEntityRepository.GetCurrentId();
            var projects = await (from up in userProjectRepository.GetAllQueryAble()
                                 join pe in projectEntityRepository.GetAllQueryAble() on up.ProjectId equals pe.Id
                                 where up.UserId == idCurrent
                                 select new GetProjectResponse()
                                 {
                                     Name = pe.Name,
                                     Id = pe.Id.Value,
                                     ImageUrl = "https://localhost:5052/Project/" + pe.ImageUrl,
                                     StartDate = pe.StartDate,
                                     EndDate = pe.EndDate,
                                     Description = pe.Description
                                 }).ToListAsync(cancellationToken);

            return new ApiResponse<List<GetProjectResponse>>() { Data = projects, Message = Message.GET_SUCCESSFULLY };

        }
    }
}
