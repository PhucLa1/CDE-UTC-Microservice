
namespace Project.Application.Features.Views.GetAnnotationByViewId
{
    public class GetAnnotationByViewIdHandler
        (IBaseRepository<Annotation> annotationRepository)
        : IQueryHandler<GetAnnotationByViewIdRequest, ApiResponse<List<GetAnnotationByViewIdResponse>>>
    {
        public async Task<ApiResponse<List<GetAnnotationByViewIdResponse>>> Handle(GetAnnotationByViewIdRequest request, CancellationToken cancellationToken)
        {
            var annotations = await annotationRepository.GetAllQueryAble()
                .Where(e => e.ViewId == request.ViewId)
                .OrderBy(e => e.Action)
                .Select(e => new GetAnnotationByViewIdResponse()
                {
                    Id = e.Id,
                    AnnotationAction = e.Action,
                    InkString = e.InkString,
                    ViewId = e.ViewId,
                })
                .ToListAsync(cancellationToken);
            annotations.Add(new GetAnnotationByViewIdResponse()
            {
                Id = 0,
                AnnotationAction = AnnotationAction.ADD,
                InkString = "",
                ViewId = request.ViewId,
            });

            return new ApiResponse<List<GetAnnotationByViewIdResponse>> () { Data = annotations, Message = Message.GET_SUCCESSFULLY };
        }
    }
}
