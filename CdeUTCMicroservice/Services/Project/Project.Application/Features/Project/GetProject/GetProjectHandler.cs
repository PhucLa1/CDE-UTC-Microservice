


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

            //Lấy ra kiểu ngày tháng mà người dùng hiện tại
            var currentDateDisplay = userProjectRepository.GetCurrentDateDisplay();
            var currenTimeDisplay = userProjectRepository.GetCurrentTimeDisplay();

            var projects = await (from up in userProjectRepository.GetAllQueryAble()
                                  join pe in projectEntityRepository.GetAllQueryAble() on up.ProjectId equals pe.Id
                                  where up.UserId == idCurrent && up.UserProjectStatus == UserProjectStatus.Active
                                  select new GetProjectResponse()
                                  {
                                      Name = pe.Name,
                                      Id = pe.Id,
                                      ImageUrl = Setting.PROJECT_HOST + "/Project/" + pe.ImageUrl,
                                      StartDate = pe.StartDate.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                                      EndDate = pe.EndDate.ConvertToFormat(currentDateDisplay, currenTimeDisplay),
                                      Description = pe.Description
                                  }).ToListAsync(cancellationToken);

            return new ApiResponse<List<GetProjectResponse>>() { Data = projects, Message = Message.GET_SUCCESSFULLY };

        }
    }
}
