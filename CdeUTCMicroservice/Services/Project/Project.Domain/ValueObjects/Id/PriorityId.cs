using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class PriorityId
    {
        public Guid Value { get; set; }
        private PriorityId(Guid value) => Value = value;
        public static PriorityId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("PriorityId cannot be empty");
            }
            return new PriorityId(value);
        }
    }
}
