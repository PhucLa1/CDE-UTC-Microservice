namespace Project.Domain.Entities
{
    public class UserProject : BaseEntity
    {
        public int UserId { get; set; }
        public int? ProjectId { get; set; } = default!;
        public Role Role { get; set; }
        public UserProjectStatus UserProjectStatus { get; set; } = UserProjectStatus.Pending;
        public DateTime LastAccessed { get; set; }
        public string EmailSend { get; set; } = string.Empty;
        public DateTime DateJoined { get; set; }
        public DateTime DateLeft { get; set; }
        
    }
}
