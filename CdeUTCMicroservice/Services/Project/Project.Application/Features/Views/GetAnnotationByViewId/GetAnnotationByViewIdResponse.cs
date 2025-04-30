namespace Project.Application.Features.Views.GetAnnotationByViewId
{
    public class GetAnnotationByViewIdResponse
    {
        public int Id { get; set; }
        public AnnotationAction AnnotationAction { get; set; }
        public string InkString { get; set; } = string.Empty;
        public int ViewId { get; set; }
    }
}
