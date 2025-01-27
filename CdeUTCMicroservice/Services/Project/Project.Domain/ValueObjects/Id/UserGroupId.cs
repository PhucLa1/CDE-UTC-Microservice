using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class UserGroupId
    {
        public Guid Value { get; set; }
        private UserGroupId(Guid value) => Value = value;
        public static UserGroupId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("UserGroupId cannot be empty");
            }
            return new UserGroupId(value);
        }
    }
}
