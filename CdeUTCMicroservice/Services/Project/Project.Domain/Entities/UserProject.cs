namespace Project.Domain.Entities
{
    public class UserProject : Entity<UserProjectId>
    {
        public Guid UserId { get; private set; }
        public ProjectId ProjectId { get; private set; } = default!;
        public Role Role { get; private set; }
        public UserProjectStatus UserProjectStatus { get; private set; } = UserProjectStatus.Pending;
        public DateTime LastAccessed { get; private set; }
        public string EmailSend { get; private set; } = default!;
        public DateTime DateJoined { get; private set; }
        public DateTime DateLeft { get; private set; }
        public static UserProject Create(Guid userId, ProjectId projectId, Role role, DateTime lastAccessed, string emailSend, DateTime dateJoined, DateTime dateLeft)
        {
            if (dateJoined < dateLeft)
                throw new ArgumentOutOfRangeException("Date Joined must be smaller than Date Left");
            var userProject = new UserProject
            {
                UserId = userId,
                ProjectId = projectId,
                Role = role,
                LastAccessed = lastAccessed,
                EmailSend = emailSend,
                DateJoined = dateJoined,
                DateLeft = dateLeft
            };
            return userProject;
        }
    }
}
