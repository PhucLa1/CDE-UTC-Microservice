using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class BCFTopicTagId
    {
        public Guid Value { get; private set; }
        private BCFTopicTagId(Guid value) => Value = value;
        public static BCFTopicTagId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("BCFTopicTagId cannot be empty");
            }
            return new BCFTopicTagId(value);
        }
    }
}
