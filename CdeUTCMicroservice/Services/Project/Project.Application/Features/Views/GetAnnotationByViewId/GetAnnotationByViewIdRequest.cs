namespace Project.Application.Features.Views.GetAnnotationByViewId
{
    public class GetAnnotationByViewIdRequest: IQuery<ApiResponse<List<GetAnnotationByViewIdResponse>>>
    {
        public int ViewId { get; set; }
    }
}
