namespace Project.Domain.Entities
{
    public class UserGroup : BaseEntity<UserGroupId>
    {
        public GroupId? GroupId { get; set; } = default!;
        public Guid UserId { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DateLeft { get; set; }
        
    }
}
