using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class TodoCommentId
    {
        public Guid Value { get; set; }
        private TodoCommentId(Guid value) => Value = value;
        public static TodoCommentId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("TodoCommentId cannot be empty");
            }
            return new TodoCommentId(value);
        }
    }
}
