using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class BCFCommentId
    {
        public Guid Value { get; }
        private BCFCommentId(Guid value) => Value = value;
        public static BCFCommentId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("BCFCommentId cannot be empty");
            }
            return new BCFCommentId(value);
        }
    }
}
