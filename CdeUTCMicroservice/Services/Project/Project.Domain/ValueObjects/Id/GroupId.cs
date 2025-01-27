using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class GroupId
    {
        public Guid Value { get; set; }
        private GroupId(Guid value) => Value = value;
        public static GroupId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("GroupId cannot be empty");
            }
            return new GroupId(value);
        }
    }
}
