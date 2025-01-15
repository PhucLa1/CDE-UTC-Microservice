using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class ViewCommentId
    {
        public Guid Value { get; private set; }
        private ViewCommentId(Guid value) => Value = value;
        public static ViewCommentId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ViewCommentId cannot be empty");
            }
            return new ViewCommentId(value);
        }
    }
}
