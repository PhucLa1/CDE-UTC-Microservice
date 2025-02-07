namespace Project.Domain.Entities
{
    public class UserGroup : BaseEntity
    {
        public int? GroupId { get; set; } = default!;
        public int UserId { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DateLeft { get; set; }
        
    }
}
