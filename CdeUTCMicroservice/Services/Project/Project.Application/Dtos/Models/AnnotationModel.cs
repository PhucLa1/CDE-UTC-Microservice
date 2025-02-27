namespace Project.Application.Dtos.Models
{
    public class AnnotationModel
    {
        public AnnotationAction AnnotationAction { get; set; }
        public string InkString { get; set; } = string.Empty;
        public int ViewId { get; set; }
    }
}
