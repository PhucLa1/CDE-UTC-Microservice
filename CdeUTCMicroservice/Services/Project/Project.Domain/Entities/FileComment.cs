namespace Project.Domain.Entities
{
    public class FileComment : BaseEntity
    {
        public string Content { get; set; } = default!;
        public int? FileId { get; set; }

       
    }
}
