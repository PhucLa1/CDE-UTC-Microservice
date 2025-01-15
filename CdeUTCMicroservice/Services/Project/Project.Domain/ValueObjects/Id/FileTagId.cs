using Project.Domain.Exceptions;

namespace Project.Domain.ValueObjects.Id
{
    public class FileTagId
    {
        public Guid Value { get; private set; }
        private FileTagId(Guid value) => Value = value;
        public static FileTagId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("FileTagId cannot be empty");
            }
            return new FileTagId(value);
        }
    }
}
