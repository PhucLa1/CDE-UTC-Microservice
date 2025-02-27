namespace Project.Domain.Entities
{
    public class View : BaseEntity
    {
        public int? FileId { get; set; } = default!;
        public ViewType ViewType { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Url { get; set; } = string.Empty;
        public string Annotation { get; set; } = string.Empty;
        public ICollection<Annotation>? Annotations { get; set; }
        public ICollection<ViewTag>? ViewTags { get; set; }
    }
}
