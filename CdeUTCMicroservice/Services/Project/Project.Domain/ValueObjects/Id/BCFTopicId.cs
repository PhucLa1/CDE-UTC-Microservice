using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class BCFTopicId
    {
        public Guid Value { get; set; }
        private BCFTopicId(Guid value) => Value = value;
        public static BCFTopicId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("BCFTopicId cannot be empty");
            }
            return new BCFTopicId(value);
        }
    }
}
