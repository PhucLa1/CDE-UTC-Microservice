using BuildingBlocks.Exceptions;
using Mapster;

namespace Project.Application.Features.Unit.GetUnitByProjectId
{
    public class GetUnitByProjectIdHandler
        (IBaseRepository<ProjectEntity> projectEntityRepository)
        : IQueryHandler<GetUnitByProjectIdRequest, ApiResponse<GetUnitByProjectIdResponse>>
    {
        public async Task<ApiResponse<GetUnitByProjectIdResponse>> Handle(GetUnitByProjectIdRequest request, CancellationToken cancellationToken)
        {
            var project = await projectEntityRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.ProjectId);

            if (project is null)
                throw new NotFoundException(Message.NOT_FOUND);

            var unit = project.Adapt<GetUnitByProjectIdResponse>();
            return new ApiResponse<GetUnitByProjectIdResponse>() { Data = unit, Message = Message.GET_SUCCESSFULLY };

        }
    }
}
