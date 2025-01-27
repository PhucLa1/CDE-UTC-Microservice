using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class TodoId
    {
        public Guid Value { get; set; }
        private TodoId(Guid value) => Value = value;
        public static TodoId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("TodoId cannot be empty");
            }
            return new TodoId(value);
        }
    }
}
