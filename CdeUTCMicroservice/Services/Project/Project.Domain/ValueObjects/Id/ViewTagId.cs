using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class ViewTagId
    {
        public Guid Value { get; set; }
        private ViewTagId(Guid value) => Value = value;
        public static ViewTagId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ViewTagId cannot be empty");
            }
            return new ViewTagId(value);
        }
    }
}
