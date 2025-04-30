namespace Project.Application.Features.Views.AddAnnotation
{
    public class AddAnnotationRequest : ICommand<AddAnnotationResponse>
    {
        public AnnotationAction Action { get; set; }
        public string InkString { get; set; } = string.Empty;
        public int ViewId { get; set; }
    }
}
