using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class UserProjectId
    {
        public Guid Value { get; set; }
        private UserProjectId(Guid value) => Value = value;
        public static UserProjectId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("UserProjectId cannot be empty");
            }
            return new UserProjectId(value);
        }
    }
}
