namespace Project.Domain.Entities
{
    public class UserGroup : Entity<UserGroupId>
    {
        public GroupId GroupId { get; private set; } = default!;
        public Guid UserId { get; private set; }
        public DateTime DateJoined { get; private set; }
        public DateTime DateLeft { get; private set; }
        public static UserGroup Create(GroupId groupId, Guid userId, DateTime dateJoined, DateTime dateLeft)
        {
            if (dateJoined < dateLeft)
                throw new ArgumentOutOfRangeException("Date Joined must be smaller than Date Left");
            var userGroup = new UserGroup
            {
                GroupId = groupId,
                UserId = userId,
                DateJoined = dateJoined,
                DateLeft = dateLeft
            };
            return userGroup;
        }
    }
}
