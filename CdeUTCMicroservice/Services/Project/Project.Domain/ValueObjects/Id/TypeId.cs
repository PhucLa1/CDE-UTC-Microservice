using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects
{
    public class TypeId
    {
        public Guid Value { get; set; }
        private TypeId(Guid value) => Value = value;
        public static TypeId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("TypeId cannot be empty");
            }
            return new TypeId(value);
        }
    }
}
