using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class ViewId
    {
        public Guid Value { get; private set; }
        private ViewId(Guid value) => Value = value;
        public static ViewId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ViewId cannot be empty");
            }
            return new ViewId(value);
        }
    }
}
