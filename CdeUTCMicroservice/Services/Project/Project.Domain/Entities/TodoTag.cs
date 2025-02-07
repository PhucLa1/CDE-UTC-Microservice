namespace Project.Domain.Entities
{
    public class TodoTag : BaseEntity
    {
        public int? TodoId { get; set; }
        public int? TagId { get; set; }
        
    }
}
