using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class TodoTagId
    {
        public Guid Value { get; private set; }
        private TodoTagId(Guid value) => Value = value;
        public static TodoTagId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("TodoTagId cannot be empty");
            }
            return new TodoTagId(value);
        }
    }
}
