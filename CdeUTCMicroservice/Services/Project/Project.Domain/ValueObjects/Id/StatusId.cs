using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class StatusId
    {
        public Guid Value { get; set; }
        private StatusId(Guid value) => Value = value;
        public static StatusId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("StatusId cannot be empty");
            }
            return new StatusId(value);
        }
    }
}
