namespace Project.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int? ProjectId { get; set; }
       
    }
}
