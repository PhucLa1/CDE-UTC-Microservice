namespace Project.Domain.Entities
{
    public class FileTodo : BaseEntity
    {
        public int? FileId { get; set; } = default!;
        public int? TodoId { get; set; } = default!;


    }
}
