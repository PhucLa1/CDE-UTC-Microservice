namespace Project.Domain.Entities
{
    public class Annotation : BaseEntity
    {
        public string AnnotationId { get; set; } = string.Empty;
        public AnnotationAction Action { get; set; }
        public string InkString { get; set; } = string.Empty;
        public int ViewId { get; set; }
        public View? View { get; set; }
    }
}
