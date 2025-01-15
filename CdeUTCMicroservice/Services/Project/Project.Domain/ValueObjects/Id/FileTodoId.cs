using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FileTodoId
    {
        public Guid Value { get; private set; }
        private FileTodoId(Guid value) => Value = value;
        public static FileTodoId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FileTodoId cannot be empty");
            }
            return new FileTodoId(value);
        }
    }
}
